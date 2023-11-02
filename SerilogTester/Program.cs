using ikvm.runtime;
using Serilog;
using Serilog.Sinks.MSSqlServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


try
{
    Log.Logger = new LoggerConfiguration()
    .WriteTo.MSSqlServer(
    connectionString: "Server=SEUSERVER;Database=Serilogger;User Id=sa;Integrated Security=SSPI;TrustServerCertificate=True",
    sinkOptions: new MSSqlServerSinkOptions
    {
        TableName = "Serilogger",
        AutoCreateSqlTable = true,
    })
    .WriteTo.File("C:\\Users\\Gabriel\\Desktop\\Studies\\SerilogTester\\SerilogTester\\Logs\\log.txt", outputTemplate: "{Timestamp} {Message}{NewLine: 1}{Exception: 1}")
    .CreateLogger();

    Log.Information("API Inicializando...");
    CreateHostBuilder(args).Build().Run();
}
catch(Exception ex)
{
    Log.Fatal(ex, "A aplicacao falhou inesperadamente");
}

finally
{
    Log.CloseAndFlush();
}

static IHostBuilder CreateHostBuilder(string[] args) =>
   Host.CreateDefaultBuilder(args)
   .UseSerilog()
   .ConfigureWebHostDefaults(webBuilder =>
   {
       webBuilder.UseStartup<Startup>();
   });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

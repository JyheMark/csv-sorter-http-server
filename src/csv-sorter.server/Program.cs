WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.WebHost.UseKestrel(options => options.ListenLocalhost(8080));

WebApplication app = builder.Build();

app.MapControllers();

app.Run();
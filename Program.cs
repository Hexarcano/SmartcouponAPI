using SmartcouponAPI.ConfigurationServer;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configurationManager = builder.Configuration;

ConfigureServer.ConfigureConnectionString(services, configurationManager);
ConfigureServer.RegisterDependencies(services, configurationManager);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

using CaseBridge.Domain.Ports;
using CaseBridge.Domain.Services;
using CaseBridge.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Obter a connection string da configuração (appsettings.json)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Registrar os repositórios com a connection string
builder.Services.AddScoped<IClientService, ClientService>();  
builder.Services.AddScoped<IProcessService, ProcessService>();

builder.Services.AddScoped<IClientRepository>(sp => new ClientRepository(connectionString));
builder.Services.AddScoped<IProcessRepository>(sp => new ProcessRepository(connectionString));
//builder.Services.AddScoped<IProcessClientRepository>(sp => new ProcessClientRepository(connectionString));


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

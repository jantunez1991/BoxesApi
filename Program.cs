using BoxesApi.Models;
using BoxesApi.Services;
using BoxesApi.Validators;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// FluentValidation
builder.Services.AddScoped<IValidator<Lead>, LeadValidator>();

// HttpClient con autenticación básica
builder.Services.AddHttpClient<IWorkshopService, WorkshopService>(client =>
{
    var authToken = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes("max@tecnom.com.ar:b0x3sApp"));
    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authToken);
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
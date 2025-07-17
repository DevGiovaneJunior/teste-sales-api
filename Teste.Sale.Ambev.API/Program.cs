using Microsoft.OpenApi.Models;
using Teste.Sale.Ambev.Crosscutting;
using Teste.Sale.Ambev.Crosscutting.Middleware;
using FluentValidation.AspNetCore;
using Teste.Sale.Ambev.Aplication.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<CreateSaleCommandValidator>();
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Sales CQRS API",
        Version = "v1",
        Description = "API de Vendas usando CQRS + DDD"
    });
});

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();
if (app.Environment.IsDevelopment() || app.Environment.IsStaging() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sales CQRS API V1");
        c.RoutePrefix = string.Empty; 
    });
}
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

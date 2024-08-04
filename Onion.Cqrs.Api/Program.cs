using Onion.Cqrs.Application;
using Onion.Cqrs.Application.Interface.Context;
using Onion.Cqrs.Application.Interface;
using Onion.Cqrs.Persistence.Context;
using Onion.Cqrs.Persistence.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddApplication();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<IDapperContext , DapperContext>();

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

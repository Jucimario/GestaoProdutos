using Application.GestaoProdutos.Context;
using Application.GestaoProdutos.Services.v1.Generic;
using Application.GestaoProdutos.Services.v1.Interfaces.IGenerics;
using Application.GestaoProdutos.Services.v1.Interfaces;
using Application.GestaoProdutos.Services.v1;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using FluentValidation.AspNetCore;
using FluentValidation;
using Application.GestaoProdutos.Validator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region Add services to the container.


builder.Services.AddControllers();

//Configurando o FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<ProdutoValidator>();


//Injeção de Dependencia
builder.Services.AddScoped(typeof(IBaseInterface<>), typeof(GenericService<>));
builder.Services.AddScoped<IProdutoService, ProdutoService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "REST API GESTÃO PRODUTOS",
            Version = "v1",
            Description = "Api responsável pelo gerenciamento de produtos",
            Contact = new OpenApiContact
            {
                Name = "Jucimario",
                Url = new Uri("https://github.com/jucimario")
            }
        });
});

//Conexão banco de dados MYSQL
string mySqlConnection = builder.Configuration["ConnectionStrings:MySQLConnectionString"];

builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseMySql(mySqlConnection,
                    ServerVersion.AutoDetect(mySqlConnection)));

//Inicio - Configuracao AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
#region Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DefaultModelsExpandDepth(-1); // desabilita o schemas do swagger       
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

#endregion

app.Run();

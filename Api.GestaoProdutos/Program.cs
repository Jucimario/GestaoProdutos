using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region Add services to the container.

builder.Services.AddControllers();

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


#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
#region Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

#endregion

app.Run();

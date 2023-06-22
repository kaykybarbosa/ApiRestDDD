using Microsoft.EntityFrameworkCore;
using RestApiDDD.Application;
using RestApiDDD.Application.Interfaces;
using RestApiDDD.Application.Interfaces.Mappers;
using RestApiDDD.Application.Mappers;
using RestApiDDD.Domain.Core.Interfaces.Repositiories;
using RestApiDDD.Domain.Core.Interfaces.Services;
using RestApiDDD.Infrastructure.Data;
using RestApiDDD.Infrastructure.Data.Repositories;
using RestApiDDD.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Configure DbContext
var connection = builder.Configuration.GetConnectionString("StringSql");
builder.Services.AddDbContext<ConnectionContext>(option => option.UseSqlServer(connection, b => b.MigrationsAssembly("Rest.ApiDDD.Api")));

//Configuration SwwaggerGen
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Rest API Model DDD", Version = "v1" });
});

//Configure Dependency Inject
builder.Services.AddScoped<IApplicationServiceClient, ApplicationServiceClient>();
builder.Services.AddScoped<IApplicationServiceProduct, ApplicationServiceProduct>();
builder.Services.AddScoped<IServiceClient, ServiceClient>();
builder.Services.AddScoped<IServiceProduct, ServiceProduct>();
builder.Services.AddScoped<IRepositoryClient, RepositoryClient>();
builder.Services.AddScoped<IRepositoryProduct, RepositoryProduct>();
builder.Services.AddScoped<IMapperClient, MapperClient>();
builder.Services.AddScoped<IMapperProduct, MapperProduct>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Alter here
    app.UseSwaggerUI( c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rest API Model DDD");
    });
}
    

app.UseHttpsRedirection();  

app.UseAuthorization();

app.MapControllers();

app.Run();

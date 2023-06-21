using Autofac;
using Microsoft.EntityFrameworkCore;
using RestApiDDD.Infrastructure.CrossCutting.IOC;
using RestApiDDD.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

//Configure DbContext
var connection = builder.Configuration["SqlConnection:ConnectioStringSql"];
builder.Services.AddDbContext<ConnectionContext>(options => options.UseSqlServer(connection));

//Configuration SwwaggerGen
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Rest API Model DDD", Version = "v1" });
});

//Configuration Inject Dependency
static void configureContainer(ContainerBuilder builder)
{
    builder.RegisterModule(new ModuleIOC());
}

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

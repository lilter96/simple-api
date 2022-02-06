using MongoDB.Bson.Serialization.Conventions;
using SimpleApi.Application.Services.Pet;
using SimpleApi.Data.MongoDb.Factories;
using SimpleApi.Data.MongoDb.Repositories;
using SimpleApi.Data.Persistent.Repositories;
using SimpleApi.Data.Persistent.Sql.Factories;
using SimpleApi.Data.Persistent.Sql.Repositories;
using SimpleApi.Web.Api.Extensions;
using IPetDomainService = SimpleApi.Domain.Pet.IPetService;
using PetDomainService = SimpleApi.Domain.Services.Pet.PetService;
using IUserDomainService = SimpleApi.Domain.User.IUserService;
using UserDomainService = SimpleApi.Domain.Services.User.UserService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register AutoMapper profiles

builder.Services.AddAutoMapper(config =>
{
    config.AddMaps(
        typeof(Program), 
        typeof(PetService),
        typeof(ZoneRepository));
});

// Register MongoDb convention

builder.Services.AddBsonConventionsAndClassMaps();

// Register domain services

builder.Services.AddTransient<IPetDomainService, PetDomainService>();
builder.Services.AddTransient<IUserDomainService, UserDomainService>();

// Register DbConnectionFactory

builder.Services.AddSingleton<IDbConnectionFactory, DapperDbConnectionFactory>();

// Register MongoDbClientFactory

builder.Services.AddSingleton<IMongoDbClientFactory, MongoDbClientFactory>();

// Register repositories

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IPetRepository, PetRepository>();
builder.Services.AddTransient<ZoneRepository>();

// Register application services

builder.Services.AddTransient<IPetService, PetService>();

// Register ASP.NET Services

builder.Services.AddControllers();
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

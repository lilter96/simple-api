using SimpleApi.Application.Services.Pet;
using SimpleApi.Data.Persistent.Repositories;
using SimpleApi.Data.Persistent.Sql.Factories;
using SimpleApi.Data.Persistent.Sql.Repositories;
using IPetDomainService = SimpleApi.Domain.Pet.IPetService;
using PetDomainService = SimpleApi.Domain.Services.Pet.PetService;
using IUserDomainService = SimpleApi.Domain.User.IUserService;
using UserDomainService = SimpleApi.Domain.Services.User.UserService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IPetDomainService, PetDomainService>();
builder.Services.AddTransient<IUserDomainService, UserDomainService>();

builder.Services.AddSingleton<IDbConnectionFactory, DapperDbConnectionFactory>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IPetRepository, PetRepository>();

builder.Services.AddTransient<IPetService, PetService>();

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

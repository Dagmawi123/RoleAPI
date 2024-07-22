using Microsoft.EntityFrameworkCore;
using OrgRoles.Controllers;
using OrgRoles.Models;
using OrgRoles.Models.Repos;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;
using OrgRoles.Models.Commands.Old;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<RoleContext, RoleContext>();
//builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleCommandsRepository, RoleCommandsRepository>();
builder.Services.AddScoped<IRoleQueriesRepository, RoleQueriesRepository>();
builder.Services.AddScoped<IRoleCommands, RoleCommands>();
builder.Services.AddScoped<IRoleQueries, RoleQueries>();


builder.Services.AddScoped<RoleController, RoleController>();

builder.Services.AddDbContext<RoleContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"));
});

//builder.Services.AddDbContext<RoleContext>(options => {
//    options.UseNpgsql(builder.Configuration.GetConnectionString("MyConnection"));
//    });

//AppContext.SetSwitch("Npgsql.EnableDiagnostics", true);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

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

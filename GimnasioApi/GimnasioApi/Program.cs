using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;

using Infrastructure.Data;
using Infrastructure.Services;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(dbContextOptions => dbContextOptions.UseSqlite(
    builder.Configuration["ConnectionStrings:DBConnectionString"]));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Repositories
builder.Services.AddScoped<ISuperAdminRepository, SuperAdminRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<ITrainerRepository, TrainerRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
#endregion

#region Services
builder.Services.AddScoped<ISendEmailService, SendEmailService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ISuperAdminService,SuperAdminService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ITrainerService,TrainerService>();
builder.Services.AddScoped<IClientService, ClientService>();
#endregion

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

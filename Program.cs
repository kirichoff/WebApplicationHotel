using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using HotelBookingApp.Infrastructure.Data;
using HotelBookingApp.Domain.Interfaces;
using HotelBookingApp.Domain.Services;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure DbContexts
builder.Services.AddDbContext<MasterDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MasterConnection"), 
        new MySqlServerVersion(new Version(8, 0, 21))));

builder.Services.AddDbContext<SlaveDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("SlaveConnection"), 
        new MySqlServerVersion(new Version(8, 0, 21))));

// Register repositories
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Register services
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC_Rehber.DataAcces.Context;
using MVC_Rehber.Services.RegistrarionService.Concrete;
using MVC_Rehber.Services.RegistrationService.Abstract;
using MVC_Rehber.Services.RegistrationService.Concrete;
using MVC_Rehber.Services.Repositories.Abstract;
using MVC_Rehber.Services.Repositories.Concrete;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Server=.;database=RehberDB;Integrated Security=True;"));
builder.Services.AddScoped<IRehberRepository, RehberRepository>();
builder.Services.AddScoped<IUyeRepository, UyeRepository>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<IUpdateRegistrationService, UpdateRegistrationService>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(x =>
{
    x.Password.RequireDigit = false;
    x.Password.RequireLowercase = false;
    x.Password.RequireUppercase = false;
    x.Password.RequireNonAlphanumeric = false;
    x.Password.RequiredLength = 3;
}).AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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

﻿using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions; // Importante para MySQL
using buses; // Asegúrate de usar el espacio de nombres de tu proyecto
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TurjoyContext>(options =>
    options.UseMySQL("server=localhost;database=turjoy;user=turjoyUsr;password=pass123"));


// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

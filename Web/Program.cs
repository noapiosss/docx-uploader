using System;
using Azure.Storage.Blobs;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Web.Clients;
using Web.Clients.Interfaces;
using Web.Configurations;
using Web.Models;
using Web.Services;
using Web.Services.Interfaces;
using Web.Validations;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<AppConfiguration>(builder.Configuration.GetSection("AZURE"));

builder.Services.AddSingleton(sp =>
{
    IOptionsMonitor<AppConfiguration> configuration = sp.GetRequiredService<IOptionsMonitor<AppConfiguration>>();
    return new BlobServiceClient(configuration.CurrentValue.BlobConnectionString);
});

builder.Services.AddHttpClient<ITriggerable, FunctionTrigger>()
    .SetHandlerLifetime(TimeSpan.FromSeconds(10));

builder.Services.AddSingleton<IBlobService, BlobService>();

builder.Services.AddScoped<IValidator<FileTransferRequest>, FileTransferValidation>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    _ = app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    _ = app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

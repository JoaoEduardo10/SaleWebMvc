using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<SeedingServices>();
builder.Services.AddScoped<SellerServices>();
builder.Services.AddScoped<DepartmentsServices>();

var connectionString = builder.Configuration.GetConnectionString("SalesWebMvcAppConext");

builder.Services.AddEntityFrameworkMySql()
.AddDbContext<SalesWebMvcContext>(
    options => options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 11)), builder => builder.MigrationsAssembly("SalesWebMvc"))
);

var app = builder.Build();


var enUs = new CultureInfo("en-US");
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(enUs),
    SupportedCultures = new List<CultureInfo> { enUs },
    SupportedUICultures = new List<CultureInfo> { enUs }
};

app.UseRequestLocalization(localizationOptions);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.Services.CreateScope().ServiceProvider.GetRequiredService<SeedingServices>().Seed();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

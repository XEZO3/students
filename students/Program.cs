using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using students.Data;
using students.Models;
using students.IServices;
using students.Services;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(50);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddDbContext<StudentContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefultConnection"));
});
builder.Services.AddIdentity<userAuth, IdentityRole>().AddEntityFrameworkStores<StudentContext>().AddDefaultTokenProviders(); ;
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddMvc().AddMvcLocalization(LanguageViewLocationExpanderFormat.Suffix);
builder.Services.Configure<RequestLocalizationOptions>(options => {
    var supportedLanguages = new[]
    {
    new CultureInfo("en"),
    new CultureInfo("ar"),
    };
    options.DefaultRequestCulture = new RequestCulture(culture: "ar", uiCulture: "ar");
    options.SupportedCultures = supportedLanguages;
    options.SupportedUICultures = supportedLanguages;
});
builder.Services.AddTransient<IDbInit,DbInit>();
builder.Services.AddScoped<students.IServices.IFile, students.Services.Files>();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(50);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/User/login";
    options.AccessDeniedPath = "/home/Index";
});


var app = builder.Build();
using var scope =  app.Services.CreateScope();
scope.ServiceProvider.GetRequiredService<IDbInit>().Init();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
var langOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(langOptions.Value);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using students.Data;
using students.Models;
using students.IServices;
using students.Services;

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
builder.Services.AddIdentity<userAuth, IdentityRole>().AddEntityFrameworkStores<StudentContext>();
builder.Services.AddTransient<IDbInit,DbInit>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/User/login";
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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

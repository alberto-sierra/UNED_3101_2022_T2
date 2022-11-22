using Microsoft.EntityFrameworkCore;
using Velocidad.Entities;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<VelocidadContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("VelocidadContext") ?? throw new InvalidOperationException("Connection string 'VelocidadContext' not found.")));


// Add services to the container.
builder.Services.AddControllersWithViews();

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


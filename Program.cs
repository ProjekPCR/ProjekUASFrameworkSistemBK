using Microsoft.EntityFrameworkCore;
using ProjekUASFrameworkSistemBK.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProjekUASFrameworkSistemBKContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProjekUASFrameworkSistemBKContext")));

// Tambahkan layanan lain
builder.Services.AddControllersWithViews();
builder.Services.AddSession(); // Aktifkan session (harus sebelum UseSession)

var app = builder.Build();

// Middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Untuk file CSS, JS, gambar

app.UseRouting();

app.UseSession(); // Penting: letakkan sebelum Authorization jika pakai session login
app.UseAuthorization();

// Routing default
// Routing default
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
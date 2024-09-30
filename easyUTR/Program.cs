using easyUTR.Data;
using easyUTR.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using easyUTR.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EasyUtrContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EasyUtrContext") ??
    throw new InvalidOperationException("Connection string 'EasyUtrContext' not found.")));
builder.Services.AddDbContext<EasyUtrIdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EasyUtrContext") ??
    throw new InvalidOperationException("Connection string 'EasyUtrContext' not found.")));
builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<EasyUtrIdentityContext>();

builder.Services.AddMemoryCache();
builder.Services.AddSession();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

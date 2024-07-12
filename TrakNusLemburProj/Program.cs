using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrakNusLemburProj.Areas.Identity.Data;
using TrakNusLemburProj.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DBLemburContextSampleConnection") ?? throw new InvalidOperationException("Connection string 'DBLemburContextSampleConnection' not found.");

builder.Services.AddDbContext<DBLemburContextSample>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<TrakNusLemburProjUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<DBLemburContextSample>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();

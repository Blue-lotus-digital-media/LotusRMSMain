
using LotusRMS.DataAccess;
using LotusRMS.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LotusRMSweb.Areas.Identity.Data;
using LotusRMS.DataAccess.Repository;
using LotusRMS.Models.IRepositorys;
using LotusRMSweb;
using DinkToPdf;
using DinkToPdf.Contracts;
using LotusRMS.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(7, 4, 26)), options => options.EnableRetryOnFailure()));


builder.Services.AddDatabaseDeveloperPageExceptionFilter();
/*
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();*/
builder.Services.AddIdentity<RMSUser,IdentityRole>()
            .AddDefaultUI()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
builder.Services.AddAuthentication().AddCookie( options => {
    options.Cookie.Expiration = TimeSpan.FromMinutes(20);
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
}
    );
/*builder.Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
options =>
{

    options.Cookie.Expiration = TimeSpan.FromMinutes(20);
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);

});*/

builder.Services.AddScoped<IUserClaimsPrincipalFactory<RMSUser>,
            ApplicationUserClaimsPrincipalFactory
            >();
builder.Services.AddControllersWithViews().AddNToastNotifyNoty(new NToastNotify.NotyOptions()
{
    ProgressBar=true,
    Timeout=5000,
    Theme="mint"
});
builder.Services.AddAuthorization(options =>
{
   /* options.AddPolicy("EmailID", policy =>
    policy.RequireClaim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "support@procodeguide.com"
    ));*/

    options.AddPolicy("rolecreation", policy =>
    policy.RequireRole("SuperAdmin")
    );
});

builder.Services.UseConfMgmtCore();
builder.Services.UseConfMgmtData();

var context = new CustomAssemblyLoadContext();
context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "libwkhtmltox.dll"));

builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseNToastNotify();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{ 
    endpoints.MapAreaControllerRoute(
        name: "SuperAdmin",
        areaName: "SuperAdmin",
        pattern: "SuperAdmin/{controller=Home}/{action=Index}/{id?}");
    endpoints.MapAreaControllerRoute(
        name: "Admin",

        areaName: "Admin",
        pattern: "Admin/{controller=Home}/{action=Index}/{id?}");
    endpoints.MapAreaControllerRoute(
        name: "Waiter",

        areaName: "Waiter",
        pattern: "Waiter/{controller=Home}/{action=Index}/{id?}");
    endpoints.MapAreaControllerRoute(
        name: "Cashier",

        areaName: "Cashier",
        pattern: "Cashier/{controller=Home}/{action=Index}/{id?}");
    endpoints.MapAreaControllerRoute(
        name: "Kitchen",
        areaName: "Kitchen",
        pattern: "Kitchen/{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllerRoute(
                   name: "areas",

                   pattern: "{area:exists}/{controller}/{action}/{id?}"
               );
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});
 

using (var scope = app.Services.CreateScope())
{
    await DbSeeder.SeedRolesAndSuperAdminAsync(scope.ServiceProvider);
} 
app.Run();

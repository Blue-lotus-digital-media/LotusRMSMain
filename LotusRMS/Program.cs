using LotusRMS.DataAccess;
using LotusRMS.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LotusRMS.DataAccess.Repository;
using LotusRMS.Models.IRepositorys;
using LotusRMSweb;
using DinkToPdf;
using DinkToPdf.Contracts;
using LotusRMS.Utility;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using LotusRMSweb.Hubs;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NLog.Extensions.Logging;
using MySqlConnector;
using LotusRMS.Models.EmailConfig;
using LotusRMS.Models.Service.Implementation;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.AddEventSourceLogger();
builder.Logging.AddNLog();

var logger = LoggerFactory.Create(config => { config.AddConsole(); }).CreateLogger("Program");
logger.LogInformation("Logger created");
logger.LogInformation(builder.Configuration.GetValue<string>("App:DefaultConnectionString"));

// Add services to the container.
var connectionStringBuilder = new MySqlConnectionStringBuilder(builder.Configuration.GetValue<string>("App:DefaultConnectionString"));

//connectionStringBuilder.Password = builder.Configuration.GetValue<string>("App:ServerPassword"); 

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionStringBuilder.ConnectionString, new MySqlServerVersion(new Version(8, 0, 11)), options => options.EnableRetryOnFailure()));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<RMSUser, IdentityRole>()
            .AddDefaultUI()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration.GetValue<string>("App:GoogleClientId");
        options.ClientSecret = builder.Configuration.GetValue<string>("App:GoogleClientSecrets");
        // options.SignInScheme = IdentityConstants.ExternalScheme;
        // Map the external picture claim to the internally used image claim
        options.ClaimActions.MapJsonKey("image", "picture");
    });
builder.Services.ConfigureApplicationCookie(options =>
{
    
    options.LoginPath = $"/account/login";
    options.LogoutPath = $"/account/logout";
    options.AccessDeniedPath = $"/account/accessDenied";
});
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = true;
});

/*builder.Services.AddScoped<IUserClaimsPrincipalFactory<RMSUser>,
            ApplicationUserClaimsPrincipalFactory
            >();*/
var emailConfig = new EmailConfiguration()
{
    From = builder.Configuration.GetValue<string>("EmailConfiguration:From"),
    Password = builder.Configuration.GetValue<string>("EmailConfiguration:Password"),
    Port = builder.Configuration.GetValue<int>("EmailConfiguration:Port"),
    SmtpServer = builder.Configuration.GetValue<string>("EmailConfiguration:SmtpServer"),
    UserName = builder.Configuration.GetValue<string>("EmailConfiguration:UserName"),
};

builder.Services.AddSingleton(emailConfig);
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.Configure<FormOptions>(o => {
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});

builder.Services.AddSignalR(cfg=>cfg.EnableDetailedErrors=true);
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
builder.Services.AddLazyResolution();
var context = new CustomAssemblyLoadContext();
context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "libwkhtmltox.dll"));

builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(60);//You can set Time   
});
builder.Services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.BottomRight; });
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseCookiePolicy(new CookiePolicyOptions()
{
    MinimumSameSitePolicy = SameSiteMode.Lax
});
app.UseNotyf();
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
        name: "Order",

        areaName: "Order",
        pattern: "Order/{controller=Home}/{action=Index}/{id?}");
    endpoints.MapAreaControllerRoute(

        name: "Checkout",
        areaName: "Checkout",
        pattern: "Checkout/{controller=Home}/{action=Index}/{id?}");
    endpoints.MapAreaControllerRoute(

        name: "Kitchen",
        areaName: "Kitchen",
        pattern: "Kitchen/{controller=Home}/{action=Index}/{id?}");
    endpoints.MapAreaControllerRoute(

        name: "Menu",
        areaName: "Menu",
        pattern: "Menu/{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllerRoute(
                   name: "areas",

                   pattern: "{area:exists}/{controller}/{action}/{id?}"
               );
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
    endpoints.MapHub<OrderHub>("/orderHub");
});

using (var scope = app.Services.CreateScope())
{
    await DbSeeder.SeedRolesAndSuperAdminAsync(scope.ServiceProvider);
    await DbSeeder.SeedMenuUnit(scope.ServiceProvider);
}
app.Run();

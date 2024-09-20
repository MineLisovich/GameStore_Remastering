using GameStore.BLL.Infrastrcture.Identity;
using GameStore.BLL.Predefined;
using GameStore.DAL.Domain;
using GameStore.DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

//Database
string? connectionString = builder.Configuration.GetConnectionString("DevConnection");
builder.Services.AddDbContext<GsDbContext>(config => config.UseNpgsql(connectionString, x => x.MigrationsAssembly("GameStore.DAL")));

//Config ASP engine
builder.Services.AddControllersWithViews().AddSessionStateTempDataProvider();

//Security & Identity Policy Config
builder.Services.AddAuthorization(config =>
{
    PredefinedManager pm = new();

    config.AddPolicy(IdentityUserPolicy.role_adminOnly, policy =>
    {
        policy.RequireRole(pm.AppRole.admin.Name);
    });

    config.AddPolicy(IdentityUserPolicy.role_UserOnly, policy =>
    {
        policy.RequireRole(pm.AppRole.user.Name);
    });

    config.AddPolicy(IdentityUserPolicy.role_AdminUser, policy =>
    {
        policy.RequireRole(pm.AppRole.admin.Name, pm.AppRole.user.Name);
    });

});

builder.Services.AddSession();
builder.Services.ConfigureApplicationCookie(config =>
{
    config.Cookie.Name = "GSCookie";
    config.Cookie.MaxAge = TimeSpan.FromDays(30);
    config.Cookie.HttpOnly = true;
    config.LoginPath = "/account/login";
    config.AccessDeniedPath = "/account/forbidden";
    config.SlidingExpiration = true;
});

builder.Services.AddIdentity<AppUser, IdentityRole>(config =>
{
    //� ������� ������������ ������ ���� ���������� Email
    config.User.RequireUniqueEmail = true;
    //����������� ������ ������
    config.Password.RequiredLength = 8;
    //������ ������ ��������� �����
    config.Password.RequireDigit = true;
    //������ ������ ��������� ����� ������� ��������
    config.Password.RequireLowercase = true;
    //������ ������ ��������� ����� �������� ��������
    config.Password.RequireUppercase = true;
    //������ ������ ��������� ���� ������
    config.Password.RequireNonAlphanumeric = true;
    //����������� ���������� ���������� ��������, ������� ������ ��������� ������
    config.Password.RequiredUniqueChars = 1;

    config.Lockout.AllowedForNewUsers = true;
    config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    config.Lockout.MaxFailedAccessAttempts = 5;

}).AddEntityFrameworkStores<GsDbContext>().AddDefaultTokenProviders();

//Automapper profiles

//BLL Services

//Middleware
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

var defaultCulture = new CultureInfo("ru-RU");
defaultCulture.NumberFormat.NumberDecimalSeparator = ",";
defaultCulture.DateTimeFormat.DateSeparator = "/";
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(defaultCulture),
    SupportedCultures = new List<CultureInfo> { defaultCulture },
    SupportedUICultures = new List<CultureInfo> { defaultCulture }
};
app.UseRequestLocalization(localizationOptions);

app.UseRouting();
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

//EndPoints
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//Seed user data to DB
var optionsBuilder = new DbContextOptionsBuilder<GsDbContext>();
optionsBuilder.UseNpgsql(connectionString);
using (var context2 = new GsDbContext(optionsBuilder.Options))
{
    if (context2.AppUsers.Any() is false)
    {
        ServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
        if (app.Environment.IsDevelopment() is true)
        {
            GsDbContext.CreationDefaultAccount(serviceProvider).Wait();
        }
    }
}

app.Run();

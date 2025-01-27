using GameStore.BLL.Infrastrcture.AutomapperProfiles.IdentityProfiles;
using GameStore.BLL.Infrastrcture.Identity;
using GameStore.BLL.Predefined;
using GameStore.BLL.Services.AccountServices;
using GameStore.DAL.Domain;
using GameStore.DAL.Entities.Identity;
using GameStore.WEB.Infrastrcture;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using GameStore.BLL.Services.UserProfileServices;
using GameStore.BLL.Services.EmailService;
using GameStore.BLL.Services.UserManagerServices;
using GameStore.BLL.Infrastrcture.AutomapperProfiles.DictionariesProfiles;
using GameStore.BLL.Infrastrcture.AutomapperProfiles.GamesProfiles;
using GameStore.BLL.Services.DictionariesServices;
using GameStore.DAL.Entities.Dictionaries;
using GameStore.BLL.DTO.Dictionaries;
using GameStore.BLL.Services.GamesServices;
using GameStore.BLL.Services.DevModeServices;
using GameStore.BLL.Services.HomeServices;
using GameStore.BLL.Services.ShoppingCartServices;
using GameStore.BLL.Services.CatalogServices;

var builder = WebApplication.CreateBuilder(args);

//Database
string? connectionString = builder.Configuration.GetConnectionString("DevConnection");
builder.Services.AddDbContext<GsDbContext>(config => config.UseNpgsql(connectionString, x => x.MigrationsAssembly("GameStore.DAL")));

//Config ASP engine
builder.Services.Configure<RazorViewEngineOptions>(option =>
{
    option.ViewLocationExpanders.Add(new PartialLocationExpander());
});
builder.Services.AddControllersWithViews().AddSessionStateTempDataProvider();

//Security & Identity Policy Config
builder.Services.AddAuthorization(config =>
{
    PredefinedManager pd = new();

    config.AddPolicy(IdentityUserPolicy.role_adminOnly, policy =>
    {
        policy.RequireRole(pd.AppRole.admin.Name);
    });

    config.AddPolicy(IdentityUserPolicy.role_UserOnly, policy =>
    {
        policy.RequireRole(pd.AppRole.user.Name);
    });

    config.AddPolicy(IdentityUserPolicy.role_AdminUser, policy =>
    {
        policy.RequireRole(pd.AppRole.admin.Name, pd.AppRole.user.Name);
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
    //У каждого пользователя должен быть уникальный Email
    config.User.RequireUniqueEmail = true;
    //Минимальная длинна пароля
    config.Password.RequiredLength = 8;
    //Пароль должен содержать цифры
    config.Password.RequireDigit = true;
    //Пароль должен содержать буквы нижнего регистра
    config.Password.RequireLowercase = true;
    //Пароль должен содержать буквы верхнего регистра
    config.Password.RequireUppercase = true;
    //Пароль должен содержать спец сиволы
    config.Password.RequireNonAlphanumeric = true;
    //Минимальное количество уникальных символов, которое должен содержать пароль
    config.Password.RequiredUniqueChars = 1;

    config.Lockout.AllowedForNewUsers = true;
    config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    config.Lockout.MaxFailedAccessAttempts = 5;

}).AddEntityFrameworkStores<GsDbContext>().AddDefaultTokenProviders();

//Automapper profiles
builder.Services.AddAutoMapper(x =>
{
    //Dictionaries
    x.AddProfile<GameDeveloperProfile>();
    x.AddProfile<GameLabelProfile>();
    x.AddProfile<GamePlatformProfile>();
    x.AddProfile<GenreProfile>();
    x.AddProfile<GameKeyStatusProfile>();

    //Games
    x.AddProfile<GameProfile>();
    x.AddProfile<GameKeyProfile>();
    x.AddProfile<GameScreenshotProfile>();

    //Identity
    x.AddProfile<AppUserProfile>();
    x.AddProfile<ShoppingCartProfile>();

});

//BLL Services
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IUserManagerService, UserManagerService>();
builder.Services.AddScoped(typeof(IGenericDictionaryService<>), typeof(GenericDictionaryService<>));
builder.Services.AddScoped<DictionaryService<Genre, GenreDTO>>();
builder.Services.AddScoped<DictionaryService<GameDeveloper, GameDeveloperDTO>>();
builder.Services.AddScoped<DictionaryService<GamePlatform, GamePlatformDTO>>();
builder.Services.AddScoped<DictionaryService<GameLabel, GameLabelDTO>>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IHomeService, HomeService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddScoped<ICatalogService, CatalogService>();


//DevModeServices
builder.Services.AddScoped<IAddPdGame, AddPdGame>();


//Middleware
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

var defaultCulture = new CultureInfo("ru-RU");
defaultCulture.NumberFormat.NumberDecimalSeparator = ".";
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
    name: "Admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
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

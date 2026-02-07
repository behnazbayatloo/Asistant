
using Asistant.Middleware;
using Asistant_Domain_AppService;
using Asistant_Domain_Core.CommentAgg.AppService;
using Asistant_Domain_Core.CommentAgg.Data;
using Asistant_Domain_Core.CommentAgg.Service;
using Asistant_Domain_Core.Configurations;
using Asistant_Domain_Core.HomeServiceAgg.AppServices;
using Asistant_Domain_Core.HomeServiceAgg.Data;
using Asistant_Domain_Core.HomeServiceAgg.Services;
using Asistant_Domain_Core.ImageAgg.AppService;
using Asistant_Domain_Core.ImageAgg.Data;
using Asistant_Domain_Core.ImageAgg.Service;
using Asistant_Domain_Core.InfraContracts;
using Asistant_Domain_Core.RequestAgg.AppServices;
using Asistant_Domain_Core.RequestAgg.Data;
using Asistant_Domain_Core.RequestAgg.Services;
using Asistant_Domain_Core.SuggestionAgg.AppServices;
using Asistant_Domain_Core.SuggestionAgg.Data;
using Asistant_Domain_Core.SuggestionAgg.Services;
using Asistant_Domain_Core.UserAgg.AppServices;
using Asistant_Domain_Core.UserAgg.Data;
using Asistant_Domain_Core.UserAgg.Entities;
using Asistant_Domain_Core.UserAgg.Services;
using Asistant_Domain_Service;
using Asistant_FrameWork.UIExtensions;
using Asistant_Infra_Cache.InMemoryCache;
using Asistant_Infra_Db_Sql.DbContext;
using Asistant_Infra_File.Service;
using Asistant_Infra_Repository.CommentAgg;
using Asistant_Infra_Repository.HomeServiceAgg;
using Asistant_Infra_Repository.ImageAgg;
using Asistant_Infra_Repository.RequestAgg;
using Asistant_Infra_Repository.SuggestionAgg;
using Asistant_Infra_Repository.UserAgg;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Serilog;
using System;

var builder = WebApplication.CreateBuilder(args);
#region AddServices 
builder.Services.AddMemoryCache();
builder.Services.AddScoped<ICacheService, InMemoryCacheService>();
builder.Services.AddScoped<ICommentRepository, CommentRepository> ();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ICommentAppService, CommentAppService>();
builder.Services.AddScoped<IRequestRepository, RequestRepository>();
builder.Services.AddScoped<IRequestService,RequestService>();
builder.Services.AddScoped<IRequestAppService, RequestAppService>();
builder.Services.AddScoped<ISuggestionRepository, SuggestionRepository>();
builder.Services.AddScoped<ISuggestionService,SuggestionService>();
builder.Services.AddScoped<ISuggestionAppService,SuggestionAppService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService,CustomerService>();
builder.Services.AddScoped<ICustomerAppService, CustomerAppService>();
builder.Services.AddScoped<IExpertRepository, ExpertRepository>();
builder.Services.AddScoped<IExpertService, ExpertService>();
builder.Services.AddScoped<IExpertAppService,ExpertAppService>();
builder.Services.AddScoped<ICityRepository,CityRepository>();
builder.Services.AddScoped<ICityService,CityService>();
builder.Services.AddScoped<ICityAppService,CityAppService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryAppService, CategoryAppService>();
builder.Services.AddScoped<IHomeServiceRepository, HomeServiceRepository>();
builder.Services.AddScoped<IHomeServiceService, HomeServiceService>();
builder.Services.AddScoped<IHomeServiceAppService, HomeServiceAppService>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IImageAppService, ImageAppService>();
builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();
builder.Services.AddScoped<IAppUserService,AppUserService>();
builder.Services.AddScoped<IAppUserAppService,AppUserAppService>();
builder.Services.AddScoped<IFileService, FileService>();
var siteSettings =
    builder.Configuration.GetSection("SiteSettings").Get<SiteSettings>();

builder.Services.AddSingleton(siteSettings);
#endregion
// Add services to the container.
#region DataBaseConfig
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddIdentity<AppUser, IdentityRole<int>>()
    .AddErrorDescriber<PersianIdentityErrorDescriber>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
      .AddDefaultTokenProviders();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

#endregion

#region Serilog
builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});
#endregion

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddRazorPages();

#region IdentityConfigs
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(20);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});
#endregion

#region Redis Settings


builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379"; // یا آدرس سرور ریدیس شما
    options.InstanceName = "MyProject_";
});
#endregion

#region InMemoryCache
builder.Services.AddMemoryCache();
#endregion
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
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();




app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();

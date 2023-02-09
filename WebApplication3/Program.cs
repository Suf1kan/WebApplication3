using Microsoft.AspNetCore.Identity;
using WebApplication3.Model;
using WebApplication3.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AuthDbContext>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AuthDbContext>();
builder.Services.AddDataProtection();
builder.Services.AddScoped<MemberService>();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    //options.IdleTimeout = TimeSpan.FromSeconds(5);
});

// Redirect
builder.Services.ConfigureApplicationCookie(Config =>
{
    Config.LoginPath = "/Login";
    Config.SlidingExpiration = true;
});

// Rate Limiting
builder.Services.Configure<IdentityOptions>(opts =>
{
    opts.Lockout.AllowedForNewUsers = true;
    opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(20);
    opts.Lockout.MaxFailedAccessAttempts = 3;
});

builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options =>
{
    options.Cookie.Name = "MyCookieAuth";
    options.AccessDeniedPath = "/Account/AccessDenied";

});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseStatusCodePagesWithRedirects("/Errors/{0}");

app.UseRouting();

app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

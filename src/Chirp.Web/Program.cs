using System.Runtime.CompilerServices;
using Chirp.Infrastructure.Repositories;
using Chirp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AspNet.Security.OAuth.GitHub;
using Microsoft.AspNetCore.Authentication.Cookies;
using static Microsoft.AspNetCore.Http.StatusCodes;
using CustomTokenProviders;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Chirp.Core1;
using Chirp.Infrastructure.DBContext;


var builder = WebApplication.CreateBuilder(args);

//CHIRPDBPATH
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ChirpDBContext>(options => options.UseSqlite(connectionString));

//In memory session provider with default in memory implementation of IDisibutedCache
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = "GitHub";
    })
    .AddCookie()
    .AddGitHub(o =>
    {
        o.ClientId = builder.Configuration["authentication:github:clientId"] ?? Environment.GetEnvironmentVariable("Github__ClientId");
        o.ClientSecret = builder.Configuration["authentication:github:clientSecret"] ?? Environment.GetEnvironmentVariable("Github__ClientSecret");
        o.CallbackPath = "/signin-github";
    });
builder.Services.AddHttpsRedirection(options =>
{
    //options.RedirectStatusCode = Status307TemporaryRedirect;
   // options.HttpsPort = 5000;
});



// Add services to the container.
builder.Services.AddIdentity<Author, IdentityRole<int>>(options =>
{
  options.SignIn.RequireConfirmedAccount = true;
  options.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";
})
    .AddEntityFrameworkStores<ChirpDBContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders()
    .AddTokenProvider<EmailConfirmationTokenProvider<Author>>("emailconfirmation");

builder.Services.AddRazorPages();
builder.Services.AddScoped<ICheepRepository, CheepRepository>();

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
      options.TokenLifespan = TimeSpan.FromHours(2));

builder.Services.Configure<EmailConfirmationTokenProviderOptions>(opt =>
     opt.TokenLifespan = TimeSpan.FromDays(3));



var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    // From the scope, get an instance of our database context.
    // Through the `using` keyword, we make sure to dispose it after we are done.
    using var context = scope.ServiceProvider.GetRequiredService<ChirpDBContext>();
    // Execute the migration from code.
    context.Database.Migrate();
    DbInitializer.SeedDatabase(context);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//seed database
//ChirpDBContext context = new ChirpDBContext();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication(); // Optional
app.UseAuthorization(); // Optional
app.MapRazorPages();
app.Run();

public partial class Program
{
}

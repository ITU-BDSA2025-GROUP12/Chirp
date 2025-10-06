var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Get database path from environment variable or use default
var dbPath = Environment.GetEnvironmentVariable("CHIRPDBPATH") 
    ?? Path.Combine(Path.GetTempPath(), "chirp.db");

// Register services with dependency injection
builder.Services.AddScoped<DBFacade>(_ => new DBFacade(dbPath));
builder.Services.AddScoped<ICheepService, CheepService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

app.Run();
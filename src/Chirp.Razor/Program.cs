using Chirp.Razor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<ICheepService, CheepService>();

//CHIRPDBPATH
string chirpdbpath = Environment.GetEnvironmentVariable("CHIRPDBPATH");
if (chirpdbpath == null)
{
    chirpdbpath = Path.Combine(Path.GetTempPath(), "chirp.db");
}

string connectionDS = $"Data Source={chirpdbpath}";

builder.Services.AddSingleton<DBFacade>(new DBFacade(connectionDS));


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
app.UseRouting();
app.MapRazorPages();

app.Run();
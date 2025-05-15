using AegislabsProject.Services;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment; 

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<KRSService>();

var app = builder.Build();

// Add Rotativa configuration
app.UseStaticFiles();
Rotativa.AspNetCore.RotativaConfiguration.Setup(env.WebRootPath, "Rotativa");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

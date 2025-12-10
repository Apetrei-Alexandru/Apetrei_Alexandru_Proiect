using Apetrei_Alexandru_Proiect.Data;
using Apetrei_Alexandru_Proiect.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// === Adaugam Database Context ===
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// ================================

builder.Services.AddHttpClient<ICarPricePredictionService, CarPricePredictionService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:65138");
});



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    DbInitializer.Initialize(services);
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// Ai deja MapStaticAssets(), deci nu se foloseste UseStaticFiles() 
// daca template-ul tau nu il include. Daca da, il putem adauga.

app.UseRouting();

app.UseAuthorization();

// Static assets mapping
app.MapStaticAssets();

// Controller routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();

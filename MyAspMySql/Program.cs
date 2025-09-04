using Microsoft.EntityFrameworkCore;
using MyAspMySql.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// 🔹 Add CORS service
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  // allow requests from any origin
              .AllowAnyMethod()  // allow GET, POST, PUT, DELETE
              .AllowAnyHeader(); // allow headers like Content-Type
    });
});

// Other services like Swagger, DB, etc.


// Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 36))));

// Build app
var app = builder.Build();

// ✅ Step 2 + Step 3: Initialize + Seed database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();

    // Run migrations automatically (optional but useful)
    context.Database.Migrate();

    // Seed default users / data
    DbInitializer.Initialize(context);
}

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapDefaultControllerRoute();
app.MapControllers();

app.Run();

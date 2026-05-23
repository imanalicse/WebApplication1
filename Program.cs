using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using WebApplication1.Data;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog from configuration
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register EF Core with SQLite
builder.Services.AddDbContext<WebApplication1.Data.ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

try
{
    Log.Information("Starting web host");

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseSerilogRequestLogging();

    app.UseHttpsRedirection();
    app.UseRouting();

    app.UseAuthorization();

    app.MapStaticAssets();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
        .WithStaticAssets();

    // Ensure database exists and seed sample data
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        db.Database.EnsureCreated();

        if (!db.Books.Any())
        {
            db.Books.AddRange(new[]
            {
                new Book { Id = 1, Title = "The Pragmatic Programmer", Author = "Andrew Hunt", Year = 1999, Tags = new[] { "programming", "software", "best-practices" } },
                new Book { Id = 2, Title = "Clean Code", Author = "Robert C. Martin", Year = 2008, Tags = new[] { "programming", "clean-code", "architecture" } },
                new Book { Id = 3, Title = "Design Patterns", Author = "Gamma et al.", Year = 1994, Tags = new[] { "patterns", "oop" } }
            });
            db.SaveChanges();
        }
    }

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    throw;
}
finally
{
    Log.CloseAndFlush();
}

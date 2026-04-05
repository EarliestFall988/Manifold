using Microsoft.EntityFrameworkCore;
using Api.Web.Models;

namespace Api.Web.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<WeatherForecast> WeatherForecasts => Set<WeatherForecast>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
        var rng = new Random(42);

        modelBuilder.Entity<WeatherForecast>().HasData(
            Enumerable.Range(1, 20).Select(i => new WeatherForecast
            {
                Id = i,
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(i)),
                TemperatureC = rng.Next(-20, 55),
                Summary = summaries[rng.Next(summaries.Length)]
            })
        );
    }
}

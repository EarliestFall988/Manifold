using Microsoft.EntityFrameworkCore;
using Api.Web.Database;
using Api.Web.Models;

namespace Api.Web.Controllers;

public class WeatherForecastController(AppDbContext db) : ManifoldController<WeatherForecast, int>(db)
{
    protected override DbSet<WeatherForecast> Entities => db.WeatherForecasts;
}

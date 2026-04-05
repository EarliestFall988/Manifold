using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Api.Web.Database;
using Api.Web.Models;

namespace Api.Web.Controllers;

public class WeatherForecastController(AppDbContext db) : ODataController
{
    [EnableQuery]
    public IQueryable<WeatherForecast> Get() => db.WeatherForecasts;

    [EnableQuery]
    public ActionResult<WeatherForecast> Get([FromRoute] int key)
    {
        var item = db.WeatherForecasts.FirstOrDefault(x => x.Id == key);
        return item is null ? NotFound() : Ok(item);
    }
}

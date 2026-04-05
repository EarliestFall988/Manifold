using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
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

    public async Task<ActionResult<WeatherForecast>> Post([FromBody] WeatherForecast item)
    {
        db.WeatherForecasts.Add(item);
        await db.SaveChangesAsync();
        return Created(item);
    }

    public async Task<ActionResult<WeatherForecast>> Patch([FromRoute] int key, [FromBody] Delta<WeatherForecast> delta)
    {
        var item = await db.WeatherForecasts.FindAsync(key);
        if (item is null) return NotFound();
        delta.Patch(item);
        await db.SaveChangesAsync();
        return Updated(item);
    }

    public async Task<IActionResult> Delete([FromRoute] int key)
    {
        var item = await db.WeatherForecasts.FindAsync(key);
        if (item is null) return NotFound();
        db.WeatherForecasts.Remove(item);
        await db.SaveChangesAsync();
        return NoContent();
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using Api.Web.Models;

namespace Api.Web.Controllers;

public abstract class ManifoldController<T, TKey>(DbContext db) : ODataController
    where T : class, IAudit
{
    protected abstract DbSet<T> Entities { get; }

    [EnableQuery]
    public IQueryable<T> Get() => Entities.Where(x => !x.Archived);

    [EnableQuery]
    public async Task<ActionResult<T>> Get([FromRoute] TKey key)
    {
        var item = await Entities.FindAsync(key);
        return item is null || item.Archived ? NotFound() : Ok(item);
    }

    public async Task<ActionResult<T>> Post([FromBody] T item)
    {
        item.Inserted = DateTime.UtcNow;
        item.InsertedBy = User.Identity?.Name ?? "system";
        item.Archived = false;
        Entities.Add(item);
        await db.SaveChangesAsync();
        return Created(item);
    }

    public async Task<ActionResult<T>> Patch([FromRoute] TKey key, [FromBody] Delta<T> delta)
    {
        var item = await Entities.FindAsync(key);
        if (item is null || item.Archived) return NotFound();
        delta.Patch(item);
        item.Updated = DateTime.UtcNow;
        item.UpdatedBy = User.Identity?.Name ?? "system";
        await db.SaveChangesAsync();
        return Updated(item);
    }

    public async Task<IActionResult> Delete([FromRoute] TKey key)
    {
        var item = await Entities.FindAsync(key);
        if (item is null || item.Archived) return NotFound();
        item.Archived = true;
        item.Updated = DateTime.UtcNow;
        item.UpdatedBy = User.Identity?.Name ?? "system";
        await db.SaveChangesAsync();
        return NoContent();
    }
}

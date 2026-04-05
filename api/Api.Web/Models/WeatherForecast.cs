namespace Api.Web.Models;

public class WeatherForecast : IAudit
{
    public int Id { get; set; }

    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }

    public DateTime Inserted { get; set; }
    public string InsertedBy { get; set; } = string.Empty;
    public DateTime? Updated { get; set; }
    public string? UpdatedBy { get; set; }
    public bool Archived { get; set; }
}

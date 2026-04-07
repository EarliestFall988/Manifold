



using Api.Web.Models;

public class Instructor : IAudit
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Department { get; set; } = default!;

    public DateTime Inserted { get; set; }
    public required string InsertedBy { get; set; }
    public DateTime? Updated { get; set; }
    public string? UpdatedBy { get; set; }
    public bool Archived { get; set; }
}   



using Api.Web.Models;

public class Course : IAudit
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Weeks { get; set; }
    public int Credits { get; set; }
    public DateTime Inserted { get; set; }
    public required string InsertedBy { get; set; }
    public DateTime? Updated { get; set; }
    public string? UpdatedBy { get; set; }
    public bool Archived { get; set; }
}
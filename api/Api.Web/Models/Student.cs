


using Api.Web.Models;

public class Student : IAudit
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int Age { get; set; }


    public DateTime Inserted { get; set; }
    public required string InsertedBy { get; set; }
    public DateTime? Updated { get; set; }
    public string? UpdatedBy { get; set; }
    public bool Archived { get; set; }
}
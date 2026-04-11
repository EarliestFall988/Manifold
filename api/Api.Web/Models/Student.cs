


using Api.Web.Models;

public class Student : IAudit
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int Age { get; set; }

    public string GPA { get; set; } = "0.0";

    public string Major { get; set; } = "";

    public string Email { get; set; } = "";

    public List<Course> Courses { get; set; } = [];

    public DateTime Inserted { get; set; }
    public required string InsertedBy { get; set; }
    public DateTime? Updated { get; set; }
    public string? UpdatedBy { get; set; }
    public bool Archived { get; set; }
}
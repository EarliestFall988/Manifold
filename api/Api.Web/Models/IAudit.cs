namespace Api.Web.Models;

public interface IAudit
{
    DateTime Inserted { get; set; }
    string InsertedBy { get; set; }
    DateTime? Updated { get; set; }
    string? UpdatedBy { get; set; }
    bool Archived { get; set; }
}

using Microsoft.EntityFrameworkCore;
using Api.Web.Database;
using Api.Web.Models;

namespace Api.Web.Controllers;

public class InstructorController(AppDbContext db) : ManifoldController<Instructor, int>(db)
{
    protected override DbSet<Instructor> Entities => db.Instructors;
}

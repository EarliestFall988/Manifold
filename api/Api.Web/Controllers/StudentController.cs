using Microsoft.EntityFrameworkCore;
using Api.Web.Database;
using Api.Web.Models;

namespace Api.Web.Controllers;

public class StudentController(AppDbContext db) : ManifoldController<Student, int>(db)
{
    protected override DbSet<Student> Entities => db.Students;
}

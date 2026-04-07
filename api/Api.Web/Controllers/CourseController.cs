using Microsoft.EntityFrameworkCore;
using Api.Web.Database;
using Api.Web.Models;

namespace Api.Web.Controllers;

public class CourseController(AppDbContext db) : ManifoldController<Course, int>(db)
{
    protected override DbSet<Course> Entities => db.Courses;
}

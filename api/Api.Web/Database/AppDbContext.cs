using Microsoft.EntityFrameworkCore;
using Api.Web.Models;

namespace Api.Web.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<WeatherForecast> WeatherForecasts => Set<WeatherForecast>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Instructor> Instructors => Set<Instructor>();
    public DbSet<Course> Courses => Set<Course>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
        var rng = new Random(42);

        modelBuilder.Entity<Student>().HasData(
            new Student { Id = 1,  Name = "Alice Johnson",   Age = 20, Inserted = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), InsertedBy = "seed", Archived = false },
            new Student { Id = 2,  Name = "Ben Carter",      Age = 22, Inserted = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), InsertedBy = "seed", Archived = false },
            new Student { Id = 3,  Name = "Clara Nguyen",    Age = 21, Inserted = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), InsertedBy = "seed", Archived = false },
            new Student { Id = 4,  Name = "David Kim",       Age = 23, Inserted = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), InsertedBy = "seed", Archived = false },
            new Student { Id = 5,  Name = "Elena Rossi",     Age = 20, Inserted = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), InsertedBy = "seed", Archived = false },
            new Student { Id = 6,  Name = "Frank Osei",      Age = 24, Inserted = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), InsertedBy = "seed", Archived = false },
            new Student { Id = 7,  Name = "Grace Liu",       Age = 19, Inserted = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), InsertedBy = "seed", Archived = false },
            new Student { Id = 8,  Name = "Henry Martínez",  Age = 22, Inserted = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), InsertedBy = "seed", Archived = false },
            new Student { Id = 9,  Name = "Isla Patel",      Age = 21, Inserted = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), InsertedBy = "seed", Archived = false },
            new Student { Id = 10, Name = "James Okafor",    Age = 25, Inserted = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), InsertedBy = "seed", Archived = false }
        );

        modelBuilder.Entity<Course>().HasData(
            new Course { Id = 2,  Name = "Introduction to Computer Science", Description = "Fundamentals of programming and computational thinking.", Weeks = 16, Credits = 3, Inserted = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), InsertedBy = "seed", Archived = false },
            new Course { Id = 3,  Name = "Data Structures & Algorithms",     Description = "Arrays, linked lists, trees, graphs, and algorithm analysis.", Weeks = 16, Credits = 3, Inserted = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), InsertedBy = "seed", Archived = false },
            new Course { Id = 4,  Name = "Calculus I",                       Description = "Limits, derivatives, and an introduction to integration.", Weeks = 16, Credits = 4, Inserted = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), InsertedBy = "seed", Archived = false },
            new Course { Id = 5,  Name = "Linear Algebra",                   Description = "Vectors, matrices, eigenvalues, and linear transformations.", Weeks = 16, Credits = 3, Inserted = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), InsertedBy = "seed", Archived = false },
            new Course { Id = 6,  Name = "Web Development",                  Description = "HTML, CSS, JavaScript, and modern frontend frameworks.", Weeks = 12, Credits = 3, Inserted = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), InsertedBy = "seed", Archived = false },
            new Course { Id = 7,  Name = "Database Systems",                 Description = "Relational databases, SQL, normalization, and query optimization.", Weeks = 16, Credits = 3, Inserted = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), InsertedBy = "seed", Archived = false },
            new Course { Id = 8,  Name = "Operating Systems",                Description = "Processes, memory management, file systems, and concurrency.", Weeks = 16, Credits = 3, Inserted = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), InsertedBy = "seed", Archived = false },
            new Course { Id = 9,  Name = "Machine Learning",                 Description = "Supervised and unsupervised learning, neural networks, and model evaluation.", Weeks = 16, Credits = 4, Inserted = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), InsertedBy = "seed", Archived = false },
            new Course { Id = 10, Name = "Software Engineering",             Description = "Agile methodologies, system design, testing, and project management.", Weeks = 16, Credits = 3, Inserted = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), InsertedBy = "seed", Archived = false },
            new Course { Id = 11, Name = "Computer Networks",                Description = "TCP/IP, routing, protocols, and network security fundamentals.", Weeks = 16, Credits = 3, Inserted = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), InsertedBy = "seed", Archived = false }
        );

        modelBuilder.Entity<WeatherForecast>().HasData(
            Enumerable.Range(1, 20).Select(i => new WeatherForecast
            {
                Id = i,
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(i)),
                TemperatureC = rng.Next(-20, 55),
                Summary = summaries[rng.Next(summaries.Length)]
            })
        );
    }
}

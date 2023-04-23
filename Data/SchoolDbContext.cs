using LINQ_Avance.NET.Models;
using Microsoft.EntityFrameworkCore;

namespace LINQ_Avance.NET.Data
{
    public class SchoolDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = MASTER-CHIEF;Initial Catalog=SchoolDB-LINQ;Integrated Security = True; TrustServerCertificate=True;");


        }
    }




}

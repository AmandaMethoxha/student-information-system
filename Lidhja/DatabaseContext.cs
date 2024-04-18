using Microsoft.EntityFrameworkCore;
using Lidhja; 
using Microsoft.Extensions.Options;
using System;
using System.IO;

namespace Lidhja
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentProgress> StudentProgresses { get; set; }

        // Path to the database
        private readonly string dbPath;

        public DatabaseContext()
        {
            // Assign a path to dbPath
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            dbPath = Path.Combine(path, "lidhja.db");
          

            // Ensure the database is created
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Use SQLite and the path for the database
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }
}

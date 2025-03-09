using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Data.Contexts
{
    public class CompanyDbContext : DbContext
    {
        public CompanyDbContext(): base() { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-IL19IGJ\\SQL2022;Database=CompanyDb;Trusted_Connection=True;TrustServerCertificate=True");
        }
        public DbSet<Department> Departments { get; set; }
    }
}

using Crud.Domain.Model;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.EF
{
     public class EmployeeDBContext : DbContext
    {
        public DbSet<Employee> Employee { get; set;}

        public EmployeeDBContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasKey(e => e.Id);
        }
       
    }
}

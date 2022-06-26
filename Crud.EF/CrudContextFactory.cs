using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.EF
{
   public class EmployeerContextFactory: IDesignTimeDbContextFactory<EmployeeDBContext>

    {
        public EmployeeDBContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<EmployeeDBContext>();
            options.UseSqlServer("Data Source=.\\SQLEXPRESS;User ID=SA;Password=WIN-O8APH0MLJCO;Initial Catalog=EmployeeCrudDB;Integrated Security=True;Trusted_Connection=false;");
            return new EmployeeDBContext(options.Options);
        }
    }
         
}

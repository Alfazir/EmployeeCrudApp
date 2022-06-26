using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.EF.Services
{
    public class NonqueryDataService<T> where T : class
    {
        private readonly EmployeerContextFactory _employeerContextFactory;
        private readonly NonqueryDataService<T> _nonqueryDataService;

        public NonqueryDataService(EmployeerContextFactory contextFactory)
        {
            _employeerContextFactory = contextFactory;
        }
        public async Task<T> Create (T entity)
        {
            using EmployeeDBContext context = _employeerContextFactory.CreateDbContext ();
            EntityEntry<T> createResult = await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
            return createResult.Entity;
        }
        public async Task<bool> Delete (T entity)
        {
            using EmployeeDBContext context= _employeerContextFactory.CreateDbContext ();
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<T> Update (T entity)
        {
            using EmployeeDBContext context = _employeerContextFactory.CreateDbContext();
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }
   
}

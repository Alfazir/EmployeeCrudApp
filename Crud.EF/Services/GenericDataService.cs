using Crud.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.EF.Services
{
    public class GenericDataService<T>:IDataService<T> where T:class
    {
        private readonly EmployeerContextFactory _employeerContextFactory;
        private readonly NonqueryDataService<T> _nonqueryDataService;

        public GenericDataService(EmployeerContextFactory contextFactory)
        {
            _employeerContextFactory = contextFactory;
            _nonqueryDataService = new NonqueryDataService<T>(contextFactory);
        }

        public async Task<T> Create (T entity)
        {
            return await _nonqueryDataService.Create(entity);
        }
        public async Task <bool> Delete ( T entity)
        {
            return await _nonqueryDataService.Delete(entity);
        }
        public async Task<T> Get (int id)
        {
            using EmployeeDBContext context = _employeerContextFactory.CreateDbContext();
            return await context.Set<T>().FindAsync(id);
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            using EmployeeDBContext context = _employeerContextFactory.CreateDbContext();
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> Update (T entity)
        {
            return await _nonqueryDataService.Update(entity);
        }
    }



}

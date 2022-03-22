using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Crud_oparation_employee.Models
{


    class GenericRepository<T> : IRepository<T> where T : class
    {

        private readonly EmployeeApiContext context;

        public GenericRepository()
        {
            context = new EmployeeApiContext();

        }
        public T Add(T item)
        {
            return context.Add(item).Entity;
        }

        public T Delete(T item)
        {
            return context.Remove(item).Entity;
        }

        public IReadOnlyList<T> Get(Expression<Func<T, bool>> condition = null)
        {
            var Entiries = context.Set<T>();

            if (condition != null)
            {
                return Entiries.Where(condition).ToList();
            }
            return Entiries.ToList();

        }

        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }

        public T Update(T item)
        {
            return context.Update(item).Entity;
        }
    }
}

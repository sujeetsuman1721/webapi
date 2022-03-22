using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JWT.Models
{
    public interface IRepository<T> where T : class
    {

        T Add(T item);

        T Update(T item);
        T Delete(T item);

        IReadOnlyList<T> Get(Expression<Func<T, bool>> condition = null);

        Task<int> SaveAsync();



    }

}


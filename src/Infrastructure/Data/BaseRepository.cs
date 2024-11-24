using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public BaseRepository() { }
        public T add(T entity)
        {
            return entity;
        }

    }
}

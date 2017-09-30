using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingBook.Repositories
{
    public interface IRepository<T> where T:class
    {
        IUnitOfWork UnitOfWork { get; set; }

        IQueryable<T> LookupAll();

        void Commit();

        void Create(T entity);

        void Remove(T entity);

        void Update(T entity);
    }
}

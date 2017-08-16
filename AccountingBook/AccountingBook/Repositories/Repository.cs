using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AccountingBook.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public IUnitOfWork UnitOfWork { get; set;}

        private DbSet<T> _Objectset;

        private DbSet<T> Objectset {
            get
            {
                if (_Objectset == null)
                {
                    _Objectset = UnitOfWork.Context.Set<T>();
                }
                return _Objectset;
            }
        }

        public Repository(IUnitOfWork unitOfwork)
        {
            UnitOfWork = unitOfwork;
        }

        public void Commit()
        {
            UnitOfWork.Save();
        }

        public IQueryable<T> LookupAll()
        {
            return Objectset;
        }                                

        public void Create(T entity)
        {
            Objectset.Add(entity);
        }

        public void Remove(T entity)
        {
            Objectset.Remove(entity);
        }
    }
}
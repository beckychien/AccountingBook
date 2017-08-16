using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingBook.Repositories
{
    public interface IUnitOfWork:IDisposable
    {
        DbContext Context { get; set; }

        void Save();


    }
}

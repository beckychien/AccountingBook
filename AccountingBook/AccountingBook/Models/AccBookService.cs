using AccountingBook.Models;
using AccountingBook.Models.ViewModels;
using AccountingBook.Repositories;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace AccountingBook.Service
{
    public class AccBookService
    {
        private readonly IRepository<AccountBook> _accRep;
        private readonly IRepository<Users> _usersRep;
        private readonly IUnitOfWork _unitofWork;

        public AccBookService(IUnitOfWork unitOfwork)
        {
            _unitofWork = unitOfwork;
            _accRep = new Repository<AccountBook>(unitOfwork);
            _usersRep = new Repository<Users>(unitOfwork);

        }

        /// <summary>
        /// AccountingBookViewModel查詢
        /// </summary>
        /// <returns></returns>
        public IPagedList<AccountingBookDisplay> AccBookVMLookup(int currentPage, int pageSize, DateTime? beginDate, DateTime? endDate)
        {
            IQueryable<AccountingBookDisplay> result;

            var source = _accRep.LookupAll();

            result = (from i in source
                      select new AccountingBookDisplay()
                      {
                          Guid = i.Id,
                          Amount = i.Amounttt,
                          Date = i.Dateee,
                          Category = i.Categoryyy
                      });

            if (beginDate != null && endDate != null)
            {
                result = result.Where(x => x.Date >= beginDate && x.Date < endDate);
            }

            return (result.OrderByDescending(x => x.Date)).ToPagedList(currentPage, pageSize);
        }

        public AccountingBookDisplay AccBookVMLookup(Guid id)
        {
            var source = _accRep.LookupAll();
            var result = (from i in source
                          where i.Id == id
                          select new AccountingBookDisplay()
                          {
                              Guid = i.Id,
                              Amount = i.Amounttt,
                              Date = i.Dateee,
                              Category = i.Categoryyy
                          }).FirstOrDefault();
            return result;
        }

        public AccountBook AccBookLookup(Guid id)
        {
            var source = _accRep.LookupAll();
            var result = (from i in source
                          where i.Id == id
                          select i).FirstOrDefault();
            return result;
        }

        public void AccBookInsert(AccountingBookViewModel item)
        {
            try
            {
                AccountBook accountBook = new AccountBook
                {
                    Id = Guid.NewGuid(),
                    Amounttt = item.Amount,
                    Categoryyy = (int)item.Category,
                    Dateee = item.Date,
                    Remarkkk = item.Memo
                };
                _accRep.Create(accountBook);
            }
            catch
            {
                throw;
            }
        }

        public List<AccountingBookDisplay> AccBookBalanceLookup()
        {
            IQueryable<AccountingBookDisplay> result;

            var source = _accRep.LookupAll();

            result = (from i in source
                      select new AccountingBookDisplay()
                      {
                          Guid = i.Id,
                          Amount = i.Categoryyy == 0 ? i.Amounttt * -1 : i.Amounttt,
                          Date = i.Dateee,
                          Category = i.Categoryyy
                      });

            return (result.OrderByDescending(x => x.Date).ToList());
        }

        public void Save()
        {
            _unitofWork.Save();
        }

        public bool AuthAccount(string account, string password)
        {
            try
            {
                var s = _usersRep.LookupAll();
                var i = s.Where(x => x.UserName == account && x.Password == password).FirstOrDefault();
                if (i != null)
                    return true;
                else
                    return false;
            }
            catch
            {
                throw;
            }
        }

        public void UpdateAccBook(AccountBook data)
        {
            try
            {
                _accRep.Update(data);
            }
            catch
            {
                throw;
            }
        }


    }
}

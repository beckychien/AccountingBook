using AccountingBook.Models;
using AccountingBook.Models.ViewModels;
using AccountingBook.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace AccountingBook.Service
{
    public class AccBookService
    {
        private readonly IRepository<AccountBook> _accRep;
        private readonly IUnitOfWork _unitofWork;

        public AccBookService(IUnitOfWork unitOfwork)
        {
            _unitofWork = unitOfwork;
            _accRep = new Repository<AccountBook>(unitOfwork);
        }

        /// <summary>
        /// AccountingBookViewModel查詢
        /// </summary>
        /// <returns></returns>
        public IQueryable<AccountingBookViewModel> AccBookVMLookup()
        {
            var source = _accRep.LookupAll();
            var result = (from i in source
                          select new AccountingBookViewModel()
                          {
                              Cost = i.Amounttt,
                              DT = i.Dateee,
                              Type = (i.Categoryyy == 1 ? "支出" : "收入")
                          });

            return result;
        }
    }
}
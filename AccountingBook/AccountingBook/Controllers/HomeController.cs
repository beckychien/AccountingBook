using AccountingBook.Models;
using AccountingBook.Models.ViewModels;
using AccountingBook.Repositories;
using AccountingBook.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace AccountingBook.Controllers
{
    public class HomeController : Controller
    {
        private readonly AccBookService _accbookSvc;
        private int pageSize = 20;

        public HomeController()
        {
            var unitOfWork = new EFUnitOfWork();
            _accbookSvc = new AccBookService(unitOfWork);
        }


        public ActionResult Index(int page=1)
        {
            
            int currentPage = page < 1 ? 1 : page;
            var resource = _accbookSvc.AccBookVMLookup(currentPage, pageSize);
            AccountingBookViewModel result = new AccountingBookViewModel
            {
                Date = DateTime.Now,
                AccountBookList = resource
            };
            return View(result);
        }

        [HttpPost]
        public ActionResult Index_Add(AccountingBookViewModel item, int page=1)
        {
            int currentPage = page < 1 ? 1 : page;

            if (ModelState.IsValid)
            {                
                    _accbookSvc.AccBookInsert(item);
                    _accbookSvc.Save();                
            }

            var result = _accbookSvc.AccBookVMLookup(currentPage, pageSize);
            return PartialView("_IndexPartial", result);
        }    

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}
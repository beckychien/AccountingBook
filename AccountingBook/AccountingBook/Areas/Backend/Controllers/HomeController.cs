using AccountingBook.Models.ViewModels;
using AccountingBook.Repositories;
using AccountingBook.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccountingBook.Areas.Backend.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly AccBookService _accbookSvc;
        private int pageSize = 20;


        public HomeController()
        {
            var unitOfWork = new EFUnitOfWork();
            _accbookSvc = new AccBookService(unitOfWork);
        }

        // GET: Backend/Home
        public ActionResult Index(int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;
            var resource = _accbookSvc.AccBookVMLookup(currentPage, pageSize,null,null);
     
            return View(resource);     
        }

        public ActionResult Edit(Guid id)
        {
            var resource = _accbookSvc.AccBookVMLookup(id);
            
            return View(resource);
        }

        
    }
}
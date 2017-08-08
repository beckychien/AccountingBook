using AccountingBook.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccountingBook.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<AccountingBookViewModel> AcBookLists = new List<AccountingBookViewModel>();
            AccountingBookViewModel ac1 = new AccountingBookViewModel();
            ac1.Type = "支出";
            ac1.DT = DateTime.Now.Date.AddDays(-2);
            ac1.Cost = 1000;
            AcBookLists.Add(ac1);

            AccountingBookViewModel ac2 = new AccountingBookViewModel();
            ac2.Type = "支出";
            ac2.DT = DateTime.Now.Date.AddDays(-1);
            ac2.Cost = 500;
            AcBookLists.Add(ac2);

            AccountingBookViewModel ac3 = new AccountingBookViewModel();
            ac3.Type = "支出";
            ac3.DT = DateTime.Now.Date;
            ac3.Cost = 1300;
            AcBookLists.Add(ac3);
            return View(AcBookLists);
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
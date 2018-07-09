using AccountingBook.Models.ViewModels;
using AccountingBook.Repositories;
using AccountingBook.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace AccountingBook.Controllers
{
    public class ChartsController : Controller
    {
        private readonly AccBookService _accbookSvc;

        public ChartsController()
        {
            var unitOfWork = new EFUnitOfWork();
            _accbookSvc = new AccBookService(unitOfWork);
        }

        // GET: Charts
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetBalance()
        {
            
            List<AccBalanceVM> data = new List<AccBalanceVM>();
            
            data = (from x in _accbookSvc.AccBookBalanceLookup()
                    group x by new { Year=x.Date.Year,Month=x.Date.Month } into y
                    select new AccBalanceVM
                    {
                        Date = y.Key.Year.ToString()+"年" + y.Key.Month.ToString()+"月",
                        Balance = y.Sum(x => x.Amount)
                    }).ToList();


            JObject obj = new JObject(
                new JProperty("Date",data.Select(x=>x.Date).ToList()),
                new JProperty("Balance",data.Select(x=>x.Balance).ToList())
                );
            return Content(obj.ToString(), "application/json");

        }

        [HttpPost]
        public ActionResult GetPie()
        {
            var data = (from x in _accbookSvc.AccBookBalanceLookup()
                       group x by new { x.ShowCategory } into y
                       select new
                       {
                           name =y.Key.ShowCategory,
                           value = Math.Abs(y.Sum(x => x.Amount))
                       }).ToList();
            
            string json = JsonConvert.SerializeObject(data);
            return Json(json);

        }
    }
}
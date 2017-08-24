using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccountingBook.Controllers
{
    public class ValidateController : Controller
    {
        // GET: Validate
        public ActionResult Index(string date)
        {
            bool isValidate = false;
            DateTime dt;
            if (DateTime.TryParse(date, out dt))
            {
                if (dt <= DateTime.Now.Date)
                    isValidate = true;
            }

            return Json(isValidate, JsonRequestBehavior.AllowGet);
        }        
    }
}
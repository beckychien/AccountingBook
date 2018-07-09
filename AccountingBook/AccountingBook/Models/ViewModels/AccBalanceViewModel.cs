using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AccountingBook.Models.ViewModels
{
    public class AccBalanceVM
    {

        [Display(Name = "日期")]
        public string Date { get; set; }

        [Display(Name = "餘額")]
        public decimal Balance { get; set; }
    }
}
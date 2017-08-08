using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AccountingBook.Models.ViewModels
{
    public class AccountingBookViewModel
    {
        [DisplayName("類型")]
        public string Type { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DisplayName("日期")]
        public DateTime DT { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0.##}")]
        [DisplayName("金額")]
        public decimal Cost { get; set; }
    }
}
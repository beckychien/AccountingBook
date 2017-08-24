using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccountingBook.Models.ViewModels
{
    public class AccountingBookViewModel
    {
        [Range(1, Int32.MaxValue, ErrorMessage = "請輸入正整數")]
        [Required(ErrorMessage = "金額為必填")]
        [DisplayName("金額")]        
        public int Amount { get; set; }

        [Remote("Index", "Validate", "", ErrorMessage = "不為有效日期或大於今日")]
        [Required(ErrorMessage = "日期為必填")]
        [DisplayName("日期")]
        [DataType(DataType.Date, ErrorMessage = "請輸入有效日期")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "備註為必填")]
        [MaxLength(100, ErrorMessage = "最多輸入{1}個字")]
        [DisplayName("備註")]
        public string Memo { get; set; }

        [Required(ErrorMessage = "類型為必填")]
        [DisplayName("類型")]
        public CategoryEnum Category { get; set; }

        public IPagedList<AccountingBookDisplay> AccountBookList { get; set; }

    }

    public class AccountingBookDisplay
    {
        public int Id { get; set; }

        public int Category { get; set; }

        [DisplayName("類型")]
        public string ShowCategory { get { return (Enum.GetName(typeof(CategoryEnum), Category)); } }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DisplayName("日期")]
        public DateTime Date { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0.##}")]
        [DisplayName("金額")]
        public int Amount { get; set; }
    }
}
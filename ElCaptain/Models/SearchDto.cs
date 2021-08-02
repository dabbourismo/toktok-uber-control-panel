using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElCaptain.Models
{
    public class SearchDto
    {
        [Display(Name = "من")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "هذا الحقل مطلوب")]
        public DateTime FromDate { get; set; } = DateTime.Now;

        [Display(Name = "الى")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "هذا الحقل مطلوب")]
        public DateTime ToDate { get; set; } = DateTime.Now;

        [Display(Name = "البحث")]
        public string SearchQuery { get; set; }

        public int NumberOfOrders { get; set; } = 0;
        public decimal OrdersIncome { get; set; } = 0;
    }
}
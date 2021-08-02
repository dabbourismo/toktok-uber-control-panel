using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElCaptain.Models
{
    public class PricingDto
    {
        public int Id { get; set; }

        [Display(Name = "مسافة كسر البنديرة")]
        public decimal Pricing1 { get; set; }
        [Display(Name = "سعر فتح البنديرة")]
        public decimal Pricing2 { get; set; }
        [Display(Name = "مسافة بعد الفتح")]
        public decimal Pricing3 { get; set; }
        [Display(Name = "السعر بعد الكسر")]
        public decimal Pricing4 { get; set; }
    }
}
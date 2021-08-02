using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElCaptain.Models.Enums
{
    public enum EnumDealLimit
    {
        [Display(Name = "لا محدود")]
        Unlimited = 0,
        [Display(Name = "محدد حسب الباقة")]
        Limited = 1
      
    }
}
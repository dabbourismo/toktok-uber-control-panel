using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElCaptain.Models.Enums
{
    public enum EnumOrderStatus
    {
        [Display(Name = "جاري البحث عن سائق")]
        TryingToFindDriver,
        [Display(Name = "السائق في الطريق")]
        DriverOnItsWay,
        [Display(Name = "جاري التوصيل")]
        Delivering,
        [Display(Name = "تم الانتهاء ")]
        Done,
        [Display(Name = "حجز")]
        Booked,
       
    }
}
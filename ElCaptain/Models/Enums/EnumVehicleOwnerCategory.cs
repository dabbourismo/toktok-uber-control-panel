using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElCaptain.Models.Enums
{
    public enum EnumVehicleOwnerCategory
    {
        [Display(Name = "تعامل مباشر")]
        Direct = 0,
        [Display(Name = "تعامل غير مباشر")]
        Indirect = 1
    }
}
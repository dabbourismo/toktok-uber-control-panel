using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElCaptain.Models.Enums
{
    public enum EnumVehicleType
    {
        [Display(Name = "توكتوك")]
        TokTok,
        [Display(Name = "تريسيكل")]
        Tricycle
    }
}
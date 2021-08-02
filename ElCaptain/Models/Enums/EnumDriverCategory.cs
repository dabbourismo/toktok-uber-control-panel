using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElCaptain.Models.Enums
{
    public enum EnumDriverCategory
    {
        [Display(Name = "سائق الشركة")]
        CompanyDriver = 0,
        [Display(Name = "مالك وسائق")]
        OwnerAndDriver = 1,
        [Display(Name = "سائق لدى احد الملاك")]
        FreelanceDriver = 2
    }
}
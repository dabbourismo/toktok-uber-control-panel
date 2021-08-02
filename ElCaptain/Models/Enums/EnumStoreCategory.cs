using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElCaptain.Models.Enums
{
    public enum EnumStoreCategory
    {
        //Restaurants,
        //Drinks,
        //Stores,
        //Library,
        //Pharmacies
        [Display(Name = "توكتوك")]
        TokTok,
        [Display(Name = "تروسيكل")]
        TriCycle
    }
}
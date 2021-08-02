using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElCaptain.Models.Enums
{
    public enum EnumNotifType
    {
        [Display(Name = "العميل")]
        Client,
        [Display(Name = "السائق")]
        DeliveryMan,
        [Display(Name = "المالك")]
        VehicleOwner
    }
}
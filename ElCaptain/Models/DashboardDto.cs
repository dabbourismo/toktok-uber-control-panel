using ElCaptain.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElCaptain.Models
{
    public class DashboardDto
    {
        [Display(Name = "من")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public DateTime FromDate { get; set; } = DateTime.Now;

        [Display(Name = "الى")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public DateTime ToDate { get; set; } = DateTime.Now;


        [Display(Name = "اسم المالك")]
        public List<SelectListItem> OwnersDropDownList { get; set; }
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }

        [Display(Name = "اسم السائق")]
        public List<SelectListItem> DeliveryMenDropDownList { get; set; }
        public int DeliveryManId { get; set; }
        public string DeliveryManName { get; set; }


        [Display(Name = "التصنيف")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public EnumStoreCategory Category { get; set; }





        public decimal TotalIncome { get; set; }

        public int NumberOfOrders { get; set; }

       
    }
}
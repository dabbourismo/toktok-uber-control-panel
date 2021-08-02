using ElCaptain.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElCaptain.Models
{
    public class NotificationDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "عنوان الاشعار (مطلوب)")]
        public string Name { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "وصف الاشعار (مطلوب)")]
        public string Description { get; set; }

        [Display(Name = "رابط")]
        public string Url { get; set; }

        [Display(Name = "الاشعار موجة الى")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public EnumNotifType NotifType { get; set; } = EnumNotifType.Client;

        [Display(Name = "تاريخ نهاية الاشعار")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "هذا الحقل مطلوب")]
        public DateTime ExpireDate { get; set; } = DateTime.Now;
    }
}
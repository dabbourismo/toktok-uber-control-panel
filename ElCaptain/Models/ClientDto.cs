using ElCaptain.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElCaptain.Models
{
    public class ClientDto
    {
        public int Id { get; set; }

        [Display(Name = "الاسم الرباعي")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Name { get; set; } 

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "اسم المستخدم (مطلوب)")]
        // [Remote("CheckUserNameDublicate", "Validation", HttpMethod = "GET", ErrorMessage = "اسم المستخدم موجود مسبقا", AdditionalFields = "PreviousUsername")]
        public string Username { get; set; }

        // [Required(ErrorMessage = "ادخل كلمة المرور")]
        // [StringLength(16, ErrorMessage = "يجب ان يكون الباسوورد من 8 الى 16 حرف", MinimumLength = 8)]
        [Display(Name = "كلمة المرور (مطلوب)")]
        public string Password { get; set; }

        [Display(Name = "المدينة")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public EnumCity City { get; set; } = EnumCity.Behira;

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "رقم المحمول الاول (مطلوب)")]
        [RegularExpression(@"(01)[0-9]{9}", ErrorMessage = "رقم الهاتف غير صحيح")]
        public string Phone1 { get; set; }

        [Display(Name = "رقم المحمول الثاني (مطلوب)")]
        [RegularExpression(@"(01)[0-9]{9}", ErrorMessage = "رقم الهاتف غير صحيح")]
        public string Phone2 { get; set; }

        [Display(Name = "العنوان (مطلوب)")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Address1 { get; set; }

        [Display(Name = "عنوان اضافي")]
        [DataType(DataType.MultilineText)]
        public string Address2 { get; set; }


        [Display(Name = "اقرب معلم (مطلوب)")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string NearestMonument { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RegisterDate { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("yyy-MM-dd"));

        [Display(Name = "رقم البطاقة (مطلوب)")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [RegularExpression(@"(2|3)[0-9][1-9][0-1][1-9][0-3][1-9](01|02|03|04|11|12|13|14|15|16|17|18|19|21|22|23|24|25|26|27|28|29|31|32|33|34|35|88)\d\d\d\d\d", ErrorMessage = "رقم البطاقة غير صحيح")]
        public string NationalId { get; set; }


        public bool isActive { get; set; }

        public DateTime? LastOrderDate { get; set; } 
        public int TotalOrders { get; set; }

    }
}
using ElCaptain.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElCaptain.Models
{
    public class StoreDto
    {
        public int Id { get; set; }

        [Display(Name = "التصنيف")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public EnumStoreCategory Category { get; set; }

        [Display(Name = "اسم المتجر (مطلوب)")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Name { get; set; }

        [Display(Name = "الوصف (مطلوب)")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Description { get; set; }

        [Display(Name = "العنوان (مطلوب)")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Address { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "رقم المحمول الاول (مطلوب)")]
        [RegularExpression(@"(01)[0-9]{9}", ErrorMessage = "رقم الهاتف غير صحيح")]
        public string Phone1 { get; set; }

        [Display(Name = "رقم المحمول الثاني")]
        [RegularExpression(@"(01)[0-9]{9}", ErrorMessage = "رقم الهاتف غير صحيح")]
        public string Phone2 { get; set; }

        [Display(Name = "المدينة")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public EnumCity City { get; set; } = EnumCity.Behira;

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RegisterDate { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("yyy-MM-dd"));

        [Display(Name = "Latitude")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public double Latitude { get; set; }

        [Display(Name = "Longitude")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public double Longitude { get; set; }

        [Display(Name = "مجاني")]
        public bool isFree { get; set; } = false;


        [Display(Name = "الصورة")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }
        public string Link { get; set; } = "http://adlink2019-001-site36.etempurl.com/OwnerNationalIdImages/";

    }
}
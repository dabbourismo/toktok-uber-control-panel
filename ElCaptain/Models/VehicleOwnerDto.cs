using ElCaptain.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElCaptain.Models
{
    public class VehicleOwnerDto
    {

        public int Id { get; set; }

        [Display(Name = "الاسم الرباعي")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Name { get; set; }

        [Display(Name = "تصنيف المالك")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public EnumVehicleOwnerCategory Category { get; set; } = EnumVehicleOwnerCategory.Direct;

        [Display(Name = "التليفون")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Phone { get; set; }

        [Display(Name = "العنوان")]
        public string Address { get; set; }

        [Display(Name = "رقم البطاقة")]
        public string NationalId { get; set; }

        [Display(Name = "اسم المستخدم")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Username { get; set; }


        [Display(Name = "كلمة المرور")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Password { get; set; }

        [Display(Name = "مفعل؟")]
        public bool isActive { get; set; } = true;


        public DateTime RegisterDate { get; set; } = DateTime.Now.ToUniversalTime().AddHours(2);

        public int NumberOfVehicles { get; set; } = 0;


        [Display(Name = "الصورة")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }
        public string Link { get; set; } = "http://adlink2019-001-site36.etempurl.com/OwnerNationalIdImages/";

        [Display(Name = "صورة عقد الاتفاق")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload2 { get; set; }
        public string Link2{ get; set; } = "http://adlink2019-001-site36.etempurl.com/VehicleOwnerContract/";

    }
}
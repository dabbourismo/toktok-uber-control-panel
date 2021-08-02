
using ElCaptain.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElCaptain.Models
{
    public class DeliveryManDto
    {
        public int Id { get; set; }

        [Display(Name = "اسم المستخدم")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Username { get; set; }

        [Display(Name = "كلمة المرور")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Password { get; set; }

        [Display(Name = "الاسم")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Name { get; set; }

        [Display(Name = "رقم البطاقة (مطلوب)")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        //[RegularExpression(@"(2|3)[0-9][0-9][0-1][1-9][0-3][1-9](01|02|03|04|11|12|13|14|15|16|17|18|19|21|22|23|24|25|26|27|28|29|31|32|33|34|35|88)\d\d\d\d\d", ErrorMessage = "رقم البطاقة غير صحيح")]
        public string NationalId { get; set; }

        [Display(Name = "المدينة")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public EnumCity City { get; set; }

        [Display(Name = "تصنيف السائق")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public EnumDriverCategory Category { get; set; } = EnumDriverCategory.CompanyDriver;

        [Display(Name = "عدد الرحلات")]
        public int NumberOfTrips { get; set; } = 0;

        [Display(Name = "رقم الشاسية")]
        public int ChassieNumber { get; set; } = 0;


        [Display(Name = "رقم الموتور")]
        public int MotorNumber { get; set; } = 0;


        [Display(Name = "اللون")]
        public string Color { get; set; }

        [Display(Name = "خط السير")]
        public string DrivingLine { get; set; }

        [Display(Name = "مواعيد العمل")]
        public string WorkTimes { get; set; }



        [Display(Name = "تاريخ الفحص")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime InspectionDate { get; set; } = DateTime.Now;

        [Display(Name = "نهاية الترخيص")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LicenseEndDate { get; set; } = DateTime.Now;


        [Display(Name = "نهاية الضريبة")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TaxEndDate { get; set; } = DateTime.Now;
        
        [Display(Name = "حدود التعامل بالنسبة لعدد الطلبات اليومية")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public EnumDealLimit DealLimit { get; set; } = EnumDealLimit.Unlimited;

        [Display(Name = "العنوان (مطلوب)")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Address { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "رقم المحمول الاول (مطلوب)")]
        [RegularExpression(@"(01)[0-9]{9}", ErrorMessage = "رقم الهاتف غير صحيح")]
        public string Phone1 { get; set; } = "01149219212";

        [Display(Name = "رقم المحمول الثاني")]
        [RegularExpression(@"(01)[0-9]{9}", ErrorMessage = "رقم الهاتف غير صحيح")]
        public string Phone2 { get; set; } = "01020925337";

        [Display(Name = "رقم رخصة السائق")]
        public string LandPhone { get; set; }

        [Display(Name = "تاريخ الميلاد")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; } = DateTime.Now;

        public DateTime RegisterDate { get; set; } = DateTime.Now;

        public int Rating { get; set; } = 0;

        public EnumDeliveryManStatus Status { get; set; } = EnumDeliveryManStatus.Offline;

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public bool isActive { get; set; } = true;


        //TokTok
        public int VehicleOwnerId { get; set; }
        public string VehicleOwnerName { get; set; }
        [Display(Name = "اسم المالك")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public List<SelectListItem> VehicleOwnerDropDownList { get; set; }


        [Display(Name = "نوع المركبة")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public EnumVehicleType VehicleType { get; set; } = EnumVehicleType.TokTok;

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "رقم المركبة")]
        public string VehicleNumber { get; set; }


        [Display(Name = "صورة رخصة القيادة")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload1 { get; set; }
        public string LinkDrivingId { get; set; } = "http://adlink2019-001-site36.etempurl.com/DriverImages/";


        [Display(Name = "صورة البطاقة")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload2 { get; set; }
        public string LinkNationalId { get; set; } = "http://adlink2019-001-site36.etempurl.com/DriverImages/";

        [Display(Name = "صورة رخصة المركبة")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload3 { get; set; }
        public string LinkVehicleId { get; set; } = "http://adlink2019-001-site36.etempurl.com/DriverImages/";

        [Display(Name = "صورة محضر الاستلام كعهدة شخصية")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload4 { get; set; }
        public string MagdarIstlam { get; set; } = "http://adlink2019-001-site36.etempurl.com/DriverImages/";


    }
}
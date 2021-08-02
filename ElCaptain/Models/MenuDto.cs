using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElCaptain.Models
{
    public class MenuDto
    {
        public int Id { get; set; }

        [Display(Name = "اسم المتجر")]
        public List<SelectListItem> StoresDropDownList { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }

        [Display(Name = "العنوان")]
        public string Title { get; set; }

        [Display(Name = "هل هذا عرض؟")]
        public bool isOffered { get; set; } = false;

        [Display(Name = "الصورة")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }
        public string Link { get; set; } = "http://adlink2019-001-site27.etempurl.com/imgMenus/";
    }
}
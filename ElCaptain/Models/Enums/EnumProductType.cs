using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElCaptain.Models.Enums
{
    public enum EnumProductType
    {
        [Display(Name = "محاضرة")]
        Lecture = 0,
        [Display(Name = "لايف")]
        Live = 1,
        [Display(Name = "اختبار")]
        Exam = 2,
        [Display(Name = "ملف")]
        File = 3
    }
}

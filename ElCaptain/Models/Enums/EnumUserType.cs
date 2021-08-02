using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElCaptain.Models.Enums
{
    public enum EnumUserType
    {
        [Display(Name = "طالب")]
        Student = 0,
        [Display(Name = "مدرب")]
        Trainer = 1

    }
}

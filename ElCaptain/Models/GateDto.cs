using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElCaptain.Models
{
    public class GateDto
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public string Error { get; set; } = "";
    }
}
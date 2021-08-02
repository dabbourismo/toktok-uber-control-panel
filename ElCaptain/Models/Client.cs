using ElCaptain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElCaptain.Models
{
    public class Client
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public EnumCity City { get; set; }

        public string Phone1 { get; set; }

        public string Phone2 { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string NearestMonument { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public DateTime RegisterDate { get; set; }

        public string NationalId { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }

        public bool isActive { get; set; }


    }
}
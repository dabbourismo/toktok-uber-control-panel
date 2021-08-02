using ElCaptain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElCaptain.Models
{
    public class VehicleOwner
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public EnumVehicleOwnerCategory Category { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string NationalId { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }

        public bool isActive { get; set; }

        public DateTime RegisterDate { get; set; }
    }
}
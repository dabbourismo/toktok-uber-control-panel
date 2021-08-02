using ElCaptain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElCaptain.Models
{
    public class Store
    {
        public int Id { get; set; }

        public EnumStoreCategory Category { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public string Phone1 { get; set; }

        public string Phone2 { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public EnumCity City { get; set; }

        public DateTime RegisterDate { get; set; }

        public bool isFree { get; set; }
    }
}
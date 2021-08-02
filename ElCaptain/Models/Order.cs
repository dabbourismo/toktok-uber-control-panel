using ElCaptain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElCaptain.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime Date { get; set; } = DateTime.Now.ToUniversalTime().AddHours(2);

        public int ClientId { get; set; }
        public Client Client { get; set; }

        public int? StoreId { get; set; }
        public Store Store { get; set; }

        public int? DeliveryManId { get; set; }
        public DeliveryMan DeliveryMan { get; set; }

        public string Details { get; set; }

        public string AdditionalNotes { get; set; }

        public EnumOrderStatus State { get; set; }

        public DateTime DeliveryDate { get; set; } = DateTime.Now.ToUniversalTime().AddHours(2);

        public int DeliveryManRating { get; set; }

        public int ClientRating { get; set; }

        public bool isActive { get; set; }

        public decimal Price { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string NearestMonument { get; set; }

        public string OtherPerson { get; set; }

        public double FromLatitude { get; set; }
        public double FromLongitude { get; set; }

        public double ToLatitude { get; set; }
        public double ToLongitude { get; set; }

        public string ClientNotes { get; set; }
        public string DeliveryNotes { get; set; }

        //TokTok
        public DateTime StartDate { get; set; } 
        public EnumVehicleType VehicleType { get; set; } = EnumVehicleType.TokTok;

    }
}
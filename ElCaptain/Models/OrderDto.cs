using ElCaptain.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElCaptain.Models
{
    public class OrderDto
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }


        public int ClientId { get; set; }
        public string ClientName { get; set; }

        public string ClientPhone { get; set; }

        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string StorePhone { get; set; }

        [Display(Name = "اسم السائق")]
        public List<SelectListItem> DeliveryMenDropDownList { get; set; }
        public int? DeliveryManId { get; set; }
        public string DeliveryManName { get; set; }
        public string DeliveryManPhone { get; set; }

        public string Details { get; set; }

        public string AdditionalNotes { get; set; }

        public EnumOrderStatus State { get; set; }

        public DateTime DeliveryDate { get; set; }

        public decimal Price { get; set; }





        public string Phone { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string NearestMonument { get; set; }





        public string OtherPerson { get; set; }
        public int DeliveryManRating { get; set; }
        public int ClientRating { get; set; }

        public bool isActive { get; set; }

        public double FromLatitude { get; set; }
        public double FromLongitude { get; set; }

        public double ToLatitude { get; set; }
        public double ToLongitude { get; set; }

        public string ClientNotes{ get; set; }
        public string DeliveryNotes { get; set; }

        public DateTime StartDate { get; set; }

        public double TravelTime { get; set; }





        public string VehicleNumber { get; set; }
    }
}
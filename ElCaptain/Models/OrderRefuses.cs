using ElCaptain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElCaptain.Models
{
    public class OrderRefuses
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int DeliveryManId { get; set; }
        public DeliveryMan DeliveryMan { get; set; }

        public EnumOrderRefusalType Status { get; set; }
    }
}
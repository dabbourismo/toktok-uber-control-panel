using ElCaptain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElCaptain.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public decimal Payed { get; set; }
        public EnumPaymentDirection Direction { get; set; }

        public decimal PricePerKM { get; set; }
        public DateTime Date { get; set; }
    }
}
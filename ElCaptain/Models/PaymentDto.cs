using ElCaptain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElCaptain.Models
{
    public class PaymentDto
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public string ClientName { get; set; }
        public DateTime OrderDate { get; set; }

        public decimal Payed { get; set; }
        public EnumPaymentDirection Direction { get; set; } = EnumPaymentDirection.Income;

        public decimal PricePerKM { get; set; }
        public DateTime Date { get; set; }
    }
}
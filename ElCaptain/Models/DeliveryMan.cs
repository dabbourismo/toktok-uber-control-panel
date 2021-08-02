using ElCaptain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElCaptain.Models
{
    public class DeliveryMan
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string NationalId { get; set; }

        public EnumCity City { get; set; }

        public string Address { get; set; }

        public string Phone1 { get; set; }

        public string Phone2 { get; set; }

        public string LandPhone { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime RegisterDate { get; set; }

        public int Rating { get; set; }

        public EnumDeliveryManStatus Status { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public bool isActive { get; set; }

        public DateTime LastUpdate { get; set; } = DateTime.Now;


        //TokTok
        public int VehicleOwnerId { get; set; }
        public VehicleOwner VehicleOwner { get; set; }
        

        public string VehicleNumber { get; set; }

        public EnumVehicleType VehicleType { get; set; }

        public EnumDriverCategory Category { get; set; }

        public EnumDealLimit DealLimit { get; set; }

        public int NumberOfTrips { get; set; }

        public int ChassieNumber { get; set; }

        public int MotorNumber { get; set; }

        public string Color { get; set; }

        public string TrafficDepartment { get; set; }

        public DateTime InspectionDate { get; set; }

        public DateTime LicenseEndDate { get; set; }

        public DateTime TaxEndDate { get; set; }

        public string DrivingLine { get; set; }

        public string WorkTimes { get; set; }
    }
}
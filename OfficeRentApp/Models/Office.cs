﻿using NuGet.Protocol.Plugins;
using OfficeRentApp.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace OfficeRentApp.Models
{
    public class Office
    {
        public int Id { get; set; }
        public string? BuildingName { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int Floor { get; set; }
        public decimal PricePerHour { get; set; }
        public List<Rental>? Rentals { get; set; } 
        public List<ImageManipulation>? Images{ get; set; }
        public string Location { get; set; }
        public bool HasParking { get; set; }
        public bool HasAC { get; set; }
        public bool HasWifi { get; set; }
        public bool HasCoffeeService { get; set; }


        public Office()
        {

        }

        public Office( string address, int floor, decimal price,string buildingName = "")
        {
            BuildingName = buildingName;
            Floor = floor;
            Address = address;
            PricePerHour = price;
        }
    }
}

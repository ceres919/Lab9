using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineScoreboardOfFlights.Models
{
    public abstract class ITableItem
    {
        public ITableItem(string fl_num, string dep_point, string destination, DateTime scheduled, DateTime estimated,
           string sector, string airline, string aircraftType, string image)
        {
            FlightNumber = fl_num;
            DeparturePoint = dep_point;
            Destination = destination;
            ScheduledTime = scheduled;
            EstimatedTime = estimated;
            Sector = sector;
            Airline = airline;
            AircraftType = aircraftType;
            ImageSource = image;
        }
        public string FlightNumber { get; set; }
        public string DeparturePoint { get; set; }
        public string Destination { get; set; }
        public DateTime ScheduledTime { get; set; }
        public DateTime EstimatedTime { get; set; }
        public string Sector { get; set; }
        public string Airline { get; set; }
        public string AircraftType { get; set; }
        public string ImageSource { get; set; }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineScoreboardOfFlights.Models
{
    public class DepartureTableItem : ITableItem
    {
        public DepartureTableItem(string fl_num, string dep_point, string destination, DateTime scheduled, DateTime estimated,
           string sector, string airline, string aircraftType, string image, DateTime reg_time, DateTime board_time, string gate, string reseption) : base(fl_num, dep_point, destination, scheduled, estimated, sector, airline, aircraftType, image) {
            RegistrationStartTime = reg_time;
            BoardingTime = board_time;
            GateSector = gate;
            Reseption = reseption;
        }
        public DateTime RegistrationStartTime { get; set; }
        public DateTime BoardingTime { get; set; }
        public string GateSector { get; set; }
        public string Reseption { get; set; }

    }
}

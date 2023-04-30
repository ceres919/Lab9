using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineScoreboardOfFlights.Models
{
    public class ArrivalTableItem : ITableItem
    {
        public ArrivalTableItem(string fl_num, string dep_point, string destination, DateTime scheduled, DateTime estimated,
           string sector, string airline, string aircraftType, string image, string claim_belt) : base(fl_num, dep_point, destination, scheduled, estimated, sector, airline, aircraftType, image)
        {
            BaggageClaimBelt = claim_belt;
        }
        public string BaggageClaimBelt { get; set; }

        
    }
}

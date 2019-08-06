using System;
using System.Collections.Generic;
using System.Text;

namespace DataCollection
{
    public class FlightModel
    {
        public string FromCity { get; set; }
        public string FromAirport { get; set; }
        public string FromAirportCode { get; set; }
        public string ToCity { get; set; }
        public string ToAirport { get; set; }
        public string ToAirportCode { get; set; }
        public string DeepLink { get; set; }
    }
}

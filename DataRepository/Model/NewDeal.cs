using System;
using System.Collections.Generic;
using System.Text;

namespace DataRepository.Model
{
    public class NewDeal
    {
        public string FlyFrom { get; set; }

        public string FlyTo { get; set; }

        public double PreviousPrice { get; set; }

        public double CurrentPrice { get; set; }

        public string DepartureDate { get; set; }
    }
}

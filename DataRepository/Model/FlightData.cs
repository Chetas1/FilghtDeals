using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataRepository.Model
{
    public class FlightData
    {
        [JsonProperty("data")]
        public FlightDetail[] Data { get; set; }

    }

    public class FlightDetail
    {

        [JsonProperty("flyFrom")]
        public string FlyFrom { get; set; }

        [JsonProperty("flyTo")]
        public string FlyTo { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("utc_departure")]
        public string DepartureDate { get; set; }

        [JsonProperty("cityFrom")]
        public string CityFrom { get; set; }

        [JsonProperty("cityTo")]
        public string CityTo { get; set; }

        [JsonProperty("deep_link")]
        public string DeepLink { get; set; }

        public string ReturnDate { get; set; }

        public string ScanDate { get; set; } = DateTime.Now.ToString("MM/dd/yyyy");


    }
}

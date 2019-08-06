

namespace FlightDeals
{
    using DataCollection;
    using DataCollection.BaseApi;
    using DataRepository;
    using DataRepository.Model;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;

    public static class FlightDeal
    {


        public async static void StoreFlightDetails()
        {

            List<FlightModel> flightCities = new List<FlightModel>();
            const string domain = "https://kiwicom-prod.apigee.net/v2/search";
            const string ApiKey = "pzQZdAjWhqNkCXPTYcq9dPZbJWu21RVf";
            List<string> USAAirportCode = new List<string>() { "ATL", "LAX", "ORD", "DFW", "DEN", "JFK", "SFO", "LAS", "SEA" };
            List<string> CanadaAirportCode = new List<string>() { "YYZ" };

            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://flightdealdailydatascan.s3.amazonaws.com/FlightDetails.json");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    flightCities.AddRange(JsonConvert.DeserializeObject<List<FlightModel>>(jsonString));
                }

                foreach (var flightCity in flightCities)
                {
                    try
                    {
                        if (USAAirportCode.Contains(flightCity.FromAirportCode) && USAAirportCode.Contains(flightCity.ToAirportCode))
                        {
                            for (int j = 0; j <= 180; j++)
                            {
                                for (int i = 2; i <= 14; i++)
                                {
                                    var data = await HttpRequestApi.GetDataAsync<FlightData>($"{domain}?fly_from={flightCity.FromAirportCode}&fly_to={flightCity.ToAirportCode}&date_from={DateTime.Now.ToString("dd/MM/yyyy")}&date_to={DateTime.Now.AddMonths(6).ToString("dd/MM/yyyy")}&return_from={DateTime.Now.AddDays(i).ToString("dd/MM/yyyy")}&return_to={DateTime.Now.AddDays(i).ToString("dd/MM/yyyy")}&sort=quality", ApiKey);
                                    var storeFlightScan = new List<FlightData>();
                                    foreach (var d in data.Data)
                                    {
                                        d.ReturnDate = DateTime.Now.AddDays(i).ToString("dd/MM/yyyy");
                                    }
                                    FlightRepository.StoreFlightDetails(data);
                                }
                            }
                        }
                        else if (USAAirportCode.Contains(flightCity.FromAirportCode) && USAAirportCode.Contains(flightCity.ToAirportCode))
                        {
                            for (int j = 0; j <= 180; j++)
                            {
                                for (int i = 2; i <= 14; i++)
                                {
                                    var data = await HttpRequestApi.GetDataAsync<FlightData>($"{domain}?fly_from={flightCity.FromAirportCode}&fly_to={flightCity.ToAirportCode}&date_from={DateTime.Now.ToString("dd/MM/yyyy")}&date_to={DateTime.Now.AddMonths(6).ToString("dd/MM/yyyy")}&return_from={DateTime.Now.AddDays(i).ToString("dd/MM/yyyy")}&return_to={DateTime.Now.AddDays(i).ToString("dd/MM/yyyy")}&sort=quality", ApiKey);
                                    var storeFlightScan = new List<FlightData>();
                                    foreach (var d in data.Data)
                                    {
                                        d.ReturnDate = DateTime.Now.AddDays(i).ToString("dd/MM/yyyy");
                                    }
                                    FlightRepository.StoreFlightDetails(data);
                                }
                            }
                        }
                        else
                        {
                            for (int j = 0; j <= 180; j++)
                            {
                                for (int i = 7; i <= 21; i++)
                                {
                                    string s = $"{domain}?fly_from={flightCity.FromAirportCode}&fly_to={flightCity.ToAirportCode}&date_from={DateTime.Now.AddDays(j).ToString("dd/MM/yyyy")}&date_to={DateTime.Now.AddDays(j).ToString("dd/MM/yyyy")}&return_from={DateTime.Now.AddDays(i).ToString("dd/MM/yyyy")}&return_to={DateTime.Now.AddDays(i).ToString("dd/MM/yyyy")}&sort=quality";
                                    var data = await HttpRequestApi.GetDataAsync<FlightData>($"{domain}?fly_from={flightCity.FromAirportCode}&fly_to={flightCity.ToAirportCode}&date_from={DateTime.Now.AddDays(j).ToString("dd/MM/yyyy")}&date_to={DateTime.Now.AddDays(j).ToString("dd/MM/yyyy")}&return_from={DateTime.Now.AddDays(i).ToString("dd/MM/yyyy")}&return_to={DateTime.Now.AddDays(i).ToString("dd/MM/yyyy")}&sort=quality", ApiKey);
                                    var storeFlightScan = new List<FlightData>();
                                    foreach(var d in data.Data)
                                    {
                                        d.ReturnDate = DateTime.Now.AddDays(i).ToString("dd/MM/yyyy");
                                    }
                                    FlightRepository.StoreFlightDetails(data);
                                }
                            }
                        }
                            
                    }
                    catch (Exception e)
                    {

                    }
                }
            }

            return;
        }

        //Get Flight Deals
        public static List<NewDeal> GetFlightDeals()
        {
            return FlightRepository.GetFlightDeals();
        }


        //Get Flight Deals
        public static List<NewDeal> GetFlightDealsFriAndSat()
        {
            return FlightRepository.GetFlightDealsFridayAndSaturday();
        }

    }
}

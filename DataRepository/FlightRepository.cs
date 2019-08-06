using DataRepository.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository
{
    public static class FlightRepository
    {
        static string connectionString = "Server = Chetas; Database= FlightDeals; Integrated Security = True;";
        //"Data Source=3.220.236.216;Initial Catalog=FlightDeals;Persist Security Info=True;User ID=DVP;Password=dvp";
        public static void StoreFlightDetails(FlightData listOfFlights)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;



            sqlConnection = new SqlConnection(connectionString);

            try
            {
                sqlConnection.Open();
                if (listOfFlights.Data != null && listOfFlights.Data.Length > 0)
                {
                    foreach (var flight in listOfFlights.Data)
                    {
                        try
                        {
                            flight.DeepLink = "http://www.anrdoezrs.net/links/9108882/type/dlg/" + flight.DeepLink;

                            //string sql = $"Insert into FlightScan (FlyFrom,FlyTo,Price,DepartureDate,ScanDate) values('{flight.FlyFrom}','{flight.FlyTo}',{flight.Price},'{flight.DepartureDate}','{flight.ScanDate}')";
                            string sql = $"Insert into FlightDealsCountry (FlyFrom,FlyTo,Price,DepartureDate,ScanDate,DeepLink,FromCountry,ToCountry,ReturnDate) values('{flight.FlyFrom}','{flight.FlyTo}',{flight.Price},'{flight.DepartureDate}','{flight.ScanDate}','{flight.DeepLink}','{flight.CityFrom}','{flight.CityTo}','{flight.ReturnDate}')";
                            sqlCommand = new SqlCommand(sql, sqlConnection);
                            sqlCommand.ExecuteNonQuery();
                            sqlCommand.Dispose();
                        }
                        catch (Exception e)
                        {

                        }
                    }
                }

                sqlConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("" + e.Message);
            }
        }

        public static List<NewDeal> GetFlightDeals()
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;

            sqlConnection = new SqlConnection(connectionString);

            try
            {
                sqlConnection.Open();
                string sql = $"SELECT * FROM FlightDeal where ScanDate='{DateTime.Now.ToString("yyyy-MM-dd")}' and (100 - ((CurrentPrice / PreviousPrice) * 100)) > 20";
                sqlCommand = new SqlCommand(sql, sqlConnection);
                var data = sqlCommand.ExecuteReader();
                List<NewDeal> flightDeals = new List<NewDeal>();
                while (data.Read())
                {
                    try
                    {
                        flightDeals.Add(new NewDeal
                        {
                            FlyFrom = data["FlyFrom"].ToString(),
                            FlyTo = data["FlyTo"].ToString(),
                            PreviousPrice = Convert.ToDouble(data["PreviousPrice"].ToString()),
                            CurrentPrice = Convert.ToDouble(data["CurrentPrice"].ToString()),
                            DepartureDate = data["DepartureDate"].ToString()
                        });
                    }
                    catch (Exception e)
                    {

                    }
                }

                sqlCommand.Dispose();
                Console.WriteLine("WORKED");
                sqlConnection.Close();
                return flightDeals;
            }
            catch (Exception e)
            {
                Console.WriteLine("" + e.Message);
            }
            return null;
        }

        //Domestic FLights 2 to 14 days
        //International 7 to 21 days Excluding citiy US & Canada in Departure Cloumn
        //US & Canada (Departure Colunm) 2 to 14 days
        //US-US we show price as US$ & for canada it should show cad$, 

        public static List<NewDeal> GetFlightDealsFridayAndSaturday()
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;
            sqlConnection = new SqlConnection(connectionString);

            try
            {
                sqlConnection.Open();
                string sql = $"select * from FlightDeal where DepartureDate = CAST(DATEADD(wk, DATEDIFF(wk,0,GETDATE()),5) as Date) or DepartureDate = CAST(DATEADD(wk, DATEDIFF(wk, 0, GETDATE()), 4) as Date) ";
                sqlCommand = new SqlCommand(sql, sqlConnection);
                var data = sqlCommand.ExecuteReader();
                List<NewDeal> flightDeals = new List<NewDeal>();
                while (data.Read())
                {
                    try
                    {
                        flightDeals.Add(new NewDeal
                        {
                            FlyFrom = data["FlyFrom"].ToString(),
                            FlyTo = data["FlyTo"].ToString(),
                            PreviousPrice = Convert.ToDouble(data["PreviousPrice"].ToString()),
                            CurrentPrice = Convert.ToDouble(data["CurrentPrice"].ToString()),
                            DepartureDate = data["DepartureDate"].ToString()
                        });
                    }
                    catch (Exception e)
                    {

                    }
                }

                sqlCommand.Dispose();
                sqlConnection.Close();
                return flightDeals;
            }
            catch (Exception e)
            {
                Console.WriteLine("" + e.Message);
            }
            return null;
        }
    }
}

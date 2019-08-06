using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataRepository.Model;
using Microsoft.AspNetCore.Mvc;

namespace FlightDeals.Controllers
{
    [Route("api/FlightDetails")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet()]
        public List<NewDeal> GetFlightDeals()
        {
            return FlightDeal.GetFlightDeals();
        }

        [HttpPost()]
        public void StoreFlightData()
        {
            FlightDeal.StoreFlightDetails();
        }

    }
}

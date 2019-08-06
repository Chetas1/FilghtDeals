using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using DataCollection.FacebookPost;
using DataRepository.Model;
using FlightDeals;
using Microsoft.AspNetCore.Mvc;

namespace FlightsDeals.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        [Route("storeFlightDetails")]
        public async void Get()
        {
            FlightDeal.StoreFlightDetails();
        }


        [Route("getFlightDetails")]
        public ActionResult<List<NewDeal>> Get(int id)
        {
            return FlightDeal.GetFlightDeals();
        }

        [Route("getFlightDetailsFriAndSat")]
        public ActionResult<List<NewDeal>> Get(string name)
        {
            return FlightDeal.GetFlightDealsFriAndSat();
        }

    }
}

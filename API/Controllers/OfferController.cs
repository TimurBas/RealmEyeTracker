using App;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OfferController : ControllerBase
    {
        private readonly ApiService service;

        public OfferController(ApiService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<ActionResult<List<Offer>>> GetOffersAsync(Request request)
        {
            var offers = await service.GetOffersAsync(request);
            return Ok(offers);
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetCurrentOffers()
        {
            var currentOffers = await service.GetCurrentOffers();

            return Ok(currentOffers);
        }

        [HttpGet("getItemIds")]
        public async Task<ActionResult<HashSet<string>>> GetItemIds()
        {
            var itemIds = await service.GetItemIds();

            return Ok(itemIds);
        }

        [HttpGet("getItemNames")]
        public async Task<ActionResult<HashSet<string>>> GetItemNames()
        {
            var itemNames = await service.GetItemNames();

            return Ok(itemNames);
        }
    }
}

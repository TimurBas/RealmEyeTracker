using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RealmEyeTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OfferController : ControllerBase
    {
        private const string baseUrl = "https://www.realmeye.com";
        private readonly HttpClient client;
        private readonly string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36";
        private Regex itemTitleRegex = new Regex(@"(?<=class=""item"" title="")[\w\s.':?-]+");
        private Regex itemIdSellRegex = new Regex(@"(?<=offers-to/sell/)-?\d+");
        private Regex itemIdBuyRegex = new Regex(@"(?<=offers-to/buy/)-?\d+");

        public OfferController(HttpClient client)
        {
            this.client = client;
            client.DefaultRequestHeaders.Add("User-Agent", userAgent);
        }

        [HttpPost]
        public async Task<ActionResult<string>> GetOffer(Request request)
        {
            string urlExtension;
            if (request.Selling) urlExtension = "sell";
            else urlExtension = "buy";

            var convertedItemName = request.ItemName.ToLower().Replace(" ", "");
            var itemId = Utilities.GetItemId(convertedItemName);

            var finalUrl = $"{baseUrl}/offers-to/{urlExtension}/{itemId}";

            var response = await client.GetAsync(finalUrl);

            var content = await response.Content.ReadAsStringAsync();

            return Ok(content);
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetCurrentOffers()
        {
            var finalUrl = $"{baseUrl}/current-offers";

            var response = await client.GetAsync(finalUrl);
            var content = await response.Content.ReadAsStringAsync();

            var currentOffers = FindCurrentOffers(content);

            return Ok(currentOffers);
        }

        [HttpGet("getItemIds")]
        public async Task<ActionResult<HashSet<string>>> GetItemIds()
        {
            var itemIds = new HashSet<string>();

            var currentOffers = await SearchForCurrentOffers();

            foreach (Match match in itemIdBuyRegex.Matches(currentOffers))
            {
                itemIds.Add(match.Value);
            }

            foreach (Match match in itemIdSellRegex.Matches(currentOffers))
            {
                itemIds.Add(match.Value);
            }

            return Ok(itemIds);
        }

        [HttpGet("getItemNames")]
        public async Task<ActionResult<HashSet<string>>> GetItemNames()
        {
            var itemNames = new HashSet<string>();

            var currentOffers = await SearchForCurrentOffers();

            foreach (Match match in itemTitleRegex.Matches(currentOffers))
            {
                itemNames.Add(match.Value);
            }

            return Ok(itemNames);
        }

        private string FindCurrentOffers(string content)
        {
            var targetBeginning = $"<div class=\"current-offers\">";
            var targetEnd = "</div>";
            var startIndex = 0;
            var endIndex = 0;

            for (int i = 0; i < content.Length; i++)
            {
                var prediction = content.Substring(i, targetBeginning.Length);
                if (prediction.Equals(targetBeginning))
                {
                    startIndex = i + targetBeginning.Length;
                    break;
                }
            }

            for (int i = startIndex; i < content.Length; i++)
            {
                var prediction = content.Substring(i, targetEnd.Length);
                if (prediction.Equals(targetEnd))
                {
                    endIndex = i + targetEnd.Length;
                    break;
                }
            }

            var currentOffers = content.Substring(startIndex, endIndex - startIndex);
            return currentOffers;
        }

        private async Task<string> SearchForCurrentOffers()
        {
            var currentOffersUrl = $"{baseUrl}/current-offers";

            var response = await client.GetAsync(currentOffersUrl);
            var content = await response.Content.ReadAsStringAsync();

            var contentWithApostrophe = content.Replace("&apos;", "'");

            var currentOffers = FindCurrentOffers(contentWithApostrophe);
            return currentOffers;
        }


    }
}

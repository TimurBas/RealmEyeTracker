using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common.Models;
using Common.Utilities;
using System;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OfferController : ControllerBase
    {
        private const string baseUrl = "https://www.realmeye.com";
        private readonly HttpClient client;
        private readonly string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36";
        private readonly Regex itemTitleRegex = new(@"(?<=class=""item"" title="")[\w\s.':?-]+");
        private readonly Regex itemIdSellRegex = new(@"(?<=offers-to/sell/)-?\d+");
        private readonly Regex itemIdBuyRegex = new(@"(?<=offers-to/buy/)-?\d+");
        private readonly Regex quantityRegex = new(@"\d+");
        private readonly Regex secondaryItemIdRegex = new(@"(?<=data-item="")\d+");

        public OfferController(HttpClient client)
        {
            this.client = client;
            client.DefaultRequestHeaders.Add("User-Agent", userAgent);
        }

        [HttpPost]
        public async Task<ActionResult<List<Offer>>> GetOffer(Request request)
        {
            string urlExtension;
            if (request.Selling) urlExtension = "sell";
            else urlExtension = "buy";

            var convertedItemName = request.ItemName.ToLower().Replace(" ", "");
            var itemId = Definitions.GetItemIdFromItemNameNoSpaces(convertedItemName);

            var finalUrl = $"{baseUrl}/offers-to/{urlExtension}/{itemId}";

            var response = await client.GetAsync(finalUrl);
            var content = await response.Content.ReadAsStringAsync();

            var offerRows = FindOfferRows(content, itemId, request.Selling);

            return Ok(offerRows);
        }

        private List<Offer> FindOfferRows(string content, string itemId, bool isSelling)
        {
            var offers = new List<Offer>();

            var targetBeginning = $"<tbody><tr>";
            var startIndex = 0;

            for (int i = 0; i < content.Length; i++)
            {
                var prediction = content.Substring(i, targetBeginning.Length);
                if (prediction.Equals(targetBeginning))
                {
                    startIndex = i + targetBeginning.Length;
                    break;
                }
            }

            var targetEnd = "</tr>";

            for (int i = startIndex; i < content.Length; i++)
            {
                try
                {
                    var prediction = content.Substring(i, targetEnd.Length);
                    if (prediction.Equals(targetEnd))
                    {
                        var endIndex = i + targetEnd.Length;
                        var offerHTML = content.Substring(startIndex, endIndex - startIndex);
                        var offer = AssembleOffer(offerHTML, isSelling, itemId);
                        offers.Add(offer);
                        i = endIndex;
                        startIndex = endIndex;
                    }
                } catch (Exception e)
                {
                    return offers;
                }
            }

            return offers;
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

        private Offer AssembleOffer(string offerHTML, bool isSelling, string itemId)
        {
            var quantities = FindQuantities(offerHTML);

            var offer = new Offer()
            {
                SellQuantity = quantities[0],
                BuyQuantity = quantities[1],
                AddedTime = FindAddedTime(offerHTML),
                OfferBy = FindOfferBy(offerHTML),
            };

            if (isSelling)
            {
                offer.MainItemId = itemId;
                offer.SecondaryItemId = FindSecondaryItemId(offerHTML, 2);
            }
            else
            {
                offer.MainItemId = FindSecondaryItemId(offerHTML, 1);
                offer.SecondaryItemId = itemId;
            }

            return offer; 
        }

        private string FindSecondaryItemId(string offerHTML, int v)
        {
            var rowHTML = FindRowHTML(offerHTML, v);
            var match = secondaryItemIdRegex.Match(rowHTML);
            return match.Value;
        }

        private string FindRowHTML(string offerHTML, int v)
        {
            var targetBeginning = @"<td>";
            var startIndex = 0;
            var counter = 0;

            for (int i = 0; i < offerHTML.Length; i++)
            {
                var prediction = offerHTML.Substring(i, targetBeginning.Length);
                if (prediction.Equals(targetBeginning))
                {
                    counter++;
                    if (counter == v) {
                        startIndex = i + targetBeginning.Length;
                        break;
                    }
                }
            }

            var targetEnd = @"</td>";
            var endIndex = 0;

            for (int i = startIndex; i < offerHTML.Length; i++)
            {
                var prediction = offerHTML.Substring(i, targetEnd.Length);
                if (prediction.Equals(targetEnd))
                {
                    endIndex = i;
                    break;
                }
            }

            return offerHTML.Substring(startIndex, endIndex - startIndex);
        }

        private string FindOfferBy(string offerHTML)
        {
            var targetBeginning = @"</a></td>";
            var endIndex = 0;

            for (int i = 0; i < offerHTML.Length; i++)
            {
                var prediction = offerHTML.Substring(i, targetBeginning.Length);
                if (prediction.Equals(targetBeginning))
                {
                    endIndex = i;
                    break;
                }
            }

            var targetEnd = @">";
            var startIndex = 0;

            for (int i = endIndex; i < offerHTML.Length; i--)
            {
                var prediction = offerHTML.Substring(i, targetEnd.Length);
                if (prediction.Equals(targetEnd))
                {
                    startIndex = i + 1;
                    break;
                }
            }

            return offerHTML.Substring(startIndex, endIndex - startIndex);
        }

        private string FindAddedTime(string offerHTML)
        {
            var targetBeginning = @"title=""";
            var startIndex = 0;

            for (int i = 0; i < offerHTML.Length; i++)
            {
                var prediction = offerHTML.Substring(i, targetBeginning.Length);
                if (prediction.Equals(targetBeginning))
                {
                    startIndex = i + targetBeginning.Length;
                    break;
                }
            }

            var targetEnd = @"""";
            var endIndex = 0;

            for (int i = startIndex; i < offerHTML.Length; i++)
            {
                var prediction = offerHTML.Substring(i, targetEnd.Length);
                if (prediction.Equals(targetEnd))
                {
                    endIndex = i;
                    break;
                }
            }

            return offerHTML.Substring(startIndex, endIndex - startIndex);
        }

        private List<int> FindQuantities(string offerHTML)
        {
            var quantities = new List<int>();
            var targetBeginning = @"item-quantity-static"">";
            var targetEnd = "</span>";

            for (int i = 0; i < offerHTML.Length; i++)
            {
                if (quantities.Count == 2) break;
                var predictionBeginning = offerHTML.Substring(i, targetBeginning.Length);
                if (predictionBeginning.Equals(targetBeginning))
                {
                    int startIndex = i + targetBeginning.Length;
                    for (int j = startIndex; j < offerHTML.Length; j++)
                    {
                        var predictionEnd = offerHTML.Substring(j, targetEnd.Length);
                        if (predictionEnd.Equals(targetEnd))
                        {
                            var quantitySection = offerHTML.Substring(startIndex, j - startIndex);
                            var match = quantityRegex.Match(quantitySection);
                            var quantity = int.Parse(match.Value);
                            quantities.Add(quantity);
                            break;
                        }
                    }
                }
            }
            return quantities;
        }
    }
}

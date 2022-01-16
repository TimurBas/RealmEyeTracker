﻿using Microsoft.AspNetCore.Mvc;
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
        private Regex itemTitleRegex = new(@"(?<=class=""item"" title="")[\w\s.':?-]+");
        private Regex itemIdSellRegex = new(@"(?<=offers-to/sell/)-?\d+");
        private Regex itemIdBuyRegex = new(@"(?<=offers-to/buy/)-?\d+");
        private Regex quantityRegex = new(@"\d+");

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

            var offerRows = FindOfferRows(content, itemId, request.Selling);

            return Ok(content);
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
            var counter = 0;

            for (int i = startIndex; i < content.Length; i++)
            {
                if (counter == 15) break;
                var prediction = content.Substring(i, targetEnd.Length);
                if (prediction.Equals(targetEnd))
                {
                    var endIndex = i + targetEnd.Length;
                    var offerHTML = content.Substring(startIndex, endIndex - startIndex);
                    var offer = AssembleOffer(offerHTML, isSelling);
                    offers.Add(offer);
                    counter++;
                    i = endIndex;
                    startIndex = endIndex;
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

        private Offer AssembleOffer(string offerHTML, bool isSelling)
        {
            var offer = new Offer()
            {
                //AddedTime = FindAddedTime(offerHTML),
                //OfferBy = FindOfferBy(offerHTML),
                //SecondaryItemId = FindSecondaryItemId(offerHTML)
            };
            var quantities = FindQuantities(offerHTML);

            if (isSelling)
            {
                offer.SellQuantity = quantities[0];
                offer.BuyQuantity = quantities[1];
            } else
            {
                offer.BuyQuantity = quantities[0];
                offer.SellQuantity = quantities[1];
            }

            return offer; 
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

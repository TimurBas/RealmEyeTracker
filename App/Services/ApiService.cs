using App.Utilities;
using Common.Models;
using Common.Utilities;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace App
{
    public class ApiService
    {
        private readonly HttpClient client;
        private readonly string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36";
        private const string baseUrl = "https://www.realmeye.com";

        public ApiService(HttpClient client)
        {
            this.client = client;
            client.DefaultRequestHeaders.Add("User-Agent", userAgent);
        }

        public async Task<List<Offer>> GetOffersAsync(Request request)
        {
            string urlExtension;
            if (request.Selling) urlExtension = "sell";
            else urlExtension = "buy";

            var convertedItemName = request.ItemName.ToLower().Replace(" ", "");
            var itemId = Definitions.GetItemIdFromItemNameNoSpaces(convertedItemName);

            var finalUrl = $"{baseUrl}/offers-to/{urlExtension}/{itemId}";

            var response = await client.GetAsync(finalUrl);
            var content = await response.Content.ReadAsStringAsync();

            var offerRows = ApiUtil.FindOfferRows(content, itemId, request.Selling);

            return offerRows;
        }

        public async Task<string> GetCurrentOffers()
        {
            var finalUrl = $"{baseUrl}/current-offers";

            var response = await client.GetAsync(finalUrl);
            var content = await response.Content.ReadAsStringAsync();

            var currentOffers = ApiUtil.FindCurrentOffers(content);

            return currentOffers;
        }

        public async Task<HashSet<string>> GetItemIds()
        {
            var currentOffers = await SearchForCurrentOffers();

            var itemIds = ApiUtil.FindItemIds(currentOffers);

            return itemIds;
        }

        private async Task<string> SearchForCurrentOffers()
        {
            var currentOffersUrl = $"{baseUrl}/current-offers";

            var response = await client.GetAsync(currentOffersUrl);
            var content = await response.Content.ReadAsStringAsync();

            var contentWithApostrophe = content.Replace("&apos;", "'");

            var currentOffers = ApiUtil.FindCurrentOffers(contentWithApostrophe);
            return currentOffers;
        }

        public async Task<HashSet<string>> GetItemNames()
        {
            var currentOffers = await SearchForCurrentOffers();

            var itemNames = ApiUtil.FindItemNames(currentOffers);

            return itemNames;
        }
    }
}

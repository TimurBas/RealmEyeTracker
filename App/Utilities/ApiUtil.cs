using Common.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Utilities
{
    public static class ApiUtil
    {
        private static readonly Regex itemTitleRegex = new(@"(?<=class=""item"" title="")[\w\s.':?-]+");
        private static readonly Regex itemIdSellRegex = new(@"(?<=offers-to/sell/)-?\d+");
        private static readonly Regex itemIdBuyRegex = new(@"(?<=offers-to/buy/)-?\d+");
        private static readonly Regex quantityRegex = new(@"\d+");
        private static readonly Regex secondaryItemIdRegex = new(@"(?<=data-item="")\d+");

        public static List<Offer> FindOfferRows(string content, string itemId, bool isSelling)
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
                }
                catch (Exception e)
                {
                    return offers;
                }
            }

            return offers;
        }

        private static Offer AssembleOffer(string offerHTML, bool isSelling, string itemId)
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

        private static List<int> FindQuantities(string offerHTML)
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

        private static string FindAddedTime(string offerHTML)
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

        private static string FindOfferBy(string offerHTML)
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

        private static string FindSecondaryItemId(string offerHTML, int v)
        {
            var rowHTML = FindRowHTML(offerHTML, v);
            var match = secondaryItemIdRegex.Match(rowHTML);
            return match.Value;
        }

        private static string FindRowHTML(string offerHTML, int v)
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
                    if (counter == v)
                    {
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

        internal static string FindCurrentOffers(string content)
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

        public static HashSet<string> FindItemIds(string currentOffers)
        {
            var itemIds = new HashSet<string>();

            foreach (Match match in itemIdBuyRegex.Matches(currentOffers))
            {
                itemIds.Add(match.Value);
            }

            foreach (Match match in itemIdSellRegex.Matches(currentOffers))
            {
                itemIds.Add(match.Value);
            }

            return itemIds;
        }

        public static HashSet<string> FindItemNames(string currentOffers)
        {
            var itemNames = new HashSet<string>();

            foreach (Match match in itemTitleRegex.Matches(currentOffers))
            {
                itemNames.Add(match.Value);
            }

            return itemNames;
        }
    }
}

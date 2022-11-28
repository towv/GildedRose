using System.Collections.Generic;
using System;
namespace GildedRoseKata
{
    public class GildedRose
    {
        IList<Item> Items;
        private const int MIN_QUALITY = 0;
        private const int MAX_QUALITY = 50;
        private readonly List<string> LEGENDARY_ITEMS = new List<string>() {
            "Sulfuras, Hand of Ragnaros"
        };

        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        private bool ItemIsConjured(Item item) {
            return item.Name.Contains("Conjured", StringComparison.OrdinalIgnoreCase);
        }

        private bool QualityIsPositive(Item item) {
            return item.Quality > 0;
        }

        private void IncreaseQuality(Item item, int amount) {
            var newQuality = item.Quality + amount;
            if (newQuality > MAX_QUALITY) {
                item.Quality = MAX_QUALITY;
            } else {
                item.Quality = newQuality;
            }
            
        }

        private void DecreaseQuality(Item item, int amount) {
            var newQuality = item.Quality - amount;
            if (newQuality < MIN_QUALITY) {
                item.Quality = MIN_QUALITY;
            } else {
                item.Quality = newQuality;
            }
        }

        private bool ItemIsLegendary(Item item) {
            return LEGENDARY_ITEMS.Contains(item.Name);
        }

        private bool ItemIsAgedBrie(Item item) {
            return item.Name == "Aged Brie";
        }

        private bool ItemIsBackstagePass(Item item) {
            return item.Name.Contains("Backstage pass", StringComparison.OrdinalIgnoreCase);
        }
        
        private bool ItemQualityIncreasesTheOlderItGets(Item item) {
            return ItemIsAgedBrie(item) || ItemIsBackstagePass(item);
        }

        private bool SellInHasPassed(Item item) {
            return item.SellIn < 0;
        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                if (ItemIsLegendary(item)) {
                    continue;
                }

                item.SellIn = item.SellIn - 1;
                
                if (ItemIsConjured(item)) {
                    if (SellInHasPassed(item)) {
                        DecreaseQuality(item, 4);
                    } else {
                        DecreaseQuality(item, 2);
                    }
                    continue;
                }

                if (ItemQualityIncreasesTheOlderItGets(item))
                {
                    IncreaseQuality(item, 1);

                    if (ItemIsBackstagePass(item))
                    {
                        if (item.SellIn < 11)
                        {
                            IncreaseQuality(item, 1);
                        }

                        if (item.SellIn < 6)
                        {
                            IncreaseQuality(item, 1);
                        }
                    }
                }
                else
                {
                    DecreaseQuality(item, 1);
                }

                if (SellInHasPassed(item))
                {
                    if (ItemIsAgedBrie(item)) {
                        IncreaseQuality(item, 1);
                    } else if (ItemIsBackstagePass(item)) {
                        DecreaseQuality(item, item.Quality);
                    } else {
                        DecreaseQuality(item, 1);
                    }
                }
            }
        }
    }
}

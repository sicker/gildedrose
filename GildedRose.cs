using System.Collections.Generic;

namespace csharp
{
    public class GildedRose
    {
        IList<Item> Items;

        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }


        public void UpdateQuality()
        {
            foreach (Item item in Items)
            {
                ItemUpdater updater  = ItemUpdater.Get(item.Name) ?? new NormalItemUpdater();
                updater.Update(item);
            }
        }
    }

    public class NormalItemUpdater : ItemUpdater
    {
        public override void Update(Item item)
        {
            item.SellIn--;
            if (item.SellIn < 0)
            {
                DecreaseQuality(item, 2);
            }
            else
            {
                DecreaseQuality(item);
            }
        }
    }
    public class ConjuredItemUpdater : ItemUpdater
    {
        public override void Update(Item item)
        {
            item.SellIn--;
            if (item.SellIn < 0)
            {
                DecreaseQuality(item, 4);
            }
            else
            {
                DecreaseQuality(item, 2);
            }
        }
    }

    public class AgedBrieUpdater : ItemUpdater
    {
        public override void Update(Item item)
        {
            item.SellIn--;
            if (item.SellIn < 0)
            {
                IncreaseQuality(item, 2);
            }
            else
            {
                IncreaseQuality(item);
            }
        }
    }
    public class SulfurasUpdater : ItemUpdater
    {
        public override void Update(Item item)
        {
        }
    }

    public class BackstagePassesUpdater : ItemUpdater
    {
        public override void Update(Item item)
        {
            item.SellIn--;

            if (item.SellIn < 0)
            {
                item.Quality = 0;
            }
            else if (item.SellIn < 5)
            {
                IncreaseQuality(item, 3);
            }
            else if (item.SellIn < 10)
            {
                IncreaseQuality(item, 2);
            }
            else
            {
                IncreaseQuality(item);
            }
        }
    }

    public abstract class ItemUpdater
    {
        public abstract void Update(Item item);

        protected void DecreaseQuality(Item item, int i = 1)
        {
            item.Quality -= i;
            if (item.Quality < 0)
            {
                item.Quality = 0;
            }
        }

        protected void IncreaseQuality(Item item, int i = 1)
        {
            item.Quality += i;
            if (item.Quality > 50)
            {
                item.Quality = 50;
            }
        }

        static Dictionary<string, ItemUpdater> Updaters = new Dictionary<string, ItemUpdater>
        {
            {"Aged Brie", new AgedBrieUpdater()},
            {"Backstage passes to a TAFKAL80ETC concert", new BackstagePassesUpdater()},
            {"Sulfuras, Hand of Ragnaros", new SulfurasUpdater()},
            {"Conjured", new ConjuredItemUpdater()},
        };
        public static ItemUpdater Get(string itemName)
        {
            if (Updaters.ContainsKey(itemName))
                return Updaters[itemName];
            return null;
        }
    }

}

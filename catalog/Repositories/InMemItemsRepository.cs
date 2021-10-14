using System;
using System.Collections.Generic;
using System.Linq;
using Catalog.Entities;

namespace Catalog.Repositories
{

    public class InMemItemsRepository : IItemsRepository
    {
        private readonly List<Item> items = new()
        {
            new Item{ItemId=Guid.NewGuid(),
                    Name = "Potion",
                    Price = 9,
                    CreatedDate = DateTimeOffset.UtcNow},
            new Item{ItemId=Guid.NewGuid(),
                    Name = "Iron Sword",
                    Price = 20,
                    CreatedDate = DateTimeOffset.UtcNow},
            new Item{ItemId=Guid.NewGuid(),
                    Name = "Bronze Shield",
                    Price = 18,
                    CreatedDate = DateTimeOffset.UtcNow}
        };

        public IEnumerable<Item> GetItems()
        {
            return items;
        }

        public Item GetItem(Guid id)
        {
            return items.Where(item => item.ItemId == id).SingleOrDefault();
        }
    }
}
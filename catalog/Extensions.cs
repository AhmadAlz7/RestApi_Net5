using Catalog.DTOs;
using Catalog.Entities;

namespace Catalog
{
    public static class Extensions
    {
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto
            {
                ItemDtoId = item.ItemId,
                Name = item.Name,
                Price = item.Price,
                CreatedDate = item.CreatedDate
            };
        }
    }
}
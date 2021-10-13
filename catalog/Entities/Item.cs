using System;

namespace Catalog.Entities
{
    public record Item
    {
        public Guid ItemId { get; init; }
        public string Name { get; init; }
        public decimal Price { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}
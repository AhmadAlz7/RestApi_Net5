using System;
using System.Collections.Generic;
using System.Linq;
using Catalog.DTOs;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    
    [ApiController]
    [Route("[controller]")] //GET => ./items // you can also hard-type it "[items]"
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository repositoryPointsToService;

        public ItemsController(IItemsRepository repositoryFromService)
        {
            this.repositoryPointsToService = repositoryFromService;
        }

        [HttpGet] //GET ./items
        public IEnumerable<ItemDto> GetItems()
        {
            var items = repositoryPointsToService.GetItems().Select(item => item.AsDto());

            return items;
        }

        
        [HttpGet("{id}")] //GET ./item/id
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = repositoryPointsToService.GetItem(id);
            if (item is null) return NotFound();
            
            return item.AsDto();
        }

        [HttpPost] // POST ./items
        public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto)
        {
            Item item = new(){
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };
            repositoryPointsToService.CreateItem(item);

            return CreatedAtAction(nameof(GetItem), new {id = item.Id}, item.AsDto());
        }

        [HttpPut("{id}")] //PUT ./items/{id}
        public ActionResult UpdateItem(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = repositoryPointsToService.GetItem(id);

            if(existingItem is null) return NotFound();

            Item updatedItem = existingItem with{
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            repositoryPointsToService.UpdateItem(updatedItem);

            return NoContent();
        }

        [HttpDelete("{id}")] //DELETE ./items/{id}
        public ActionResult DeleteItem(Guid id)
        {
            if(repositoryPointsToService.GetItem(id) is null) return NotFound();

            repositoryPointsToService.DeleteItem(id);

            return NoContent();
        }
    }
}
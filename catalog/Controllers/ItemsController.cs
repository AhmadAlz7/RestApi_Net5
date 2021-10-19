using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            var items = (await repositoryPointsToService.GetItemsAsync())
                        .Select(item => item.AsDto());

            return items;
        }

        
        [HttpGet("{id}")] //GET ./item/id
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
        {
            var item = await repositoryPointsToService.GetItemAsync(id);
            if (item is null) return NotFound();
            
            return item.AsDto();
        }

        [HttpPost] // POST ./items
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto)
        {
            Item item = new(){
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };
            await repositoryPointsToService.CreateItemAsync(item);

            return CreatedAtAction(nameof(GetItemAsync), new {id = item.Id}, item.AsDto());
        }

        [HttpPut("{id}")] //PUT ./items/{id}
        public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = await repositoryPointsToService.GetItemAsync(id);

            if(existingItem is null) return NotFound();

            Item updatedItem = existingItem with{
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            await repositoryPointsToService.UpdateItemAsync(updatedItem);

            return NoContent();
        }

        [HttpDelete("{id}")] //DELETE ./items/{id}
        public async Task<ActionResult> DeleteItem(Guid id)
        {
            if(repositoryPointsToService.GetItemAsync(id) is null) return NotFound();

            await repositoryPointsToService.DeleteItemAsync(id);

            return NoContent();
        }
    }
}
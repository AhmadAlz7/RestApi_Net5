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
        private readonly IItemsRepository repository;

        public ItemsController(IItemsRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet] //GET ./items
        public IEnumerable<ItemDto> GetItems()
        {
            var items = repository.GetItems().Select(item => item.AsDto());

            return items;
        }

        
        [HttpGet("{id}")] //GET ./item/id
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = repository.GetItem(id);
            if (item is null) return NotFound();
            
            return item.AsDto();
        }

    }
}
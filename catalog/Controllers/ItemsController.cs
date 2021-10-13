using System;
using System.Collections.Generic;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    
    [ApiController]
    [Route("[controller]")] //GET => ./items // you can also hard-type it "[items]"
    public class ItemsController : ControllerBase
    {
        private readonly InMemItemsRepository repository;

        public ItemsController()
        {
            repository = new InMemItemsRepository();
        }

        [HttpGet] //GET ./items
        public IEnumerable<Item> GetItems()
        {
            var items = repository.GetItems();
            return items;
        }

        
        [HttpGet("{id}")] //GET ./item/id
        public ActionResult<Item> GetItem(Guid id)
        {
            var item = repository.GetItem(id);
            if (item is null) return NotFound();
            return item;
        }

    }
}
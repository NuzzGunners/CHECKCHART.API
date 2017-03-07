using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CHECKCHART.API.Models;
using CHECKCHART.API.Abstract;

namespace CHECKCHART.API.Controllers
{
    [Route("api/[controller]")]
    public class PositionController : Controller
    {
        private IPositionRepository _items { get; set; }
        public PositionController(IPositionRepository iIPositionRepository)
        {
            _items = iIPositionRepository;
        }

        [HttpGet]
        public IEnumerable<Position> GetAll()
        {
            return _items.GetAll().OrderBy(i => i.PositionOrder);
        }

        [HttpGet("{id}", Name = "GetPosition")]
        public IActionResult GetById(int id)
        {
            var item = _items.Find(i => i.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new OkObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Position item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            _items.Add(item);
            _items.Commit();
            CreatedAtRouteResult result = CreatedAtRoute("GetPosition", new { controller = "Position", id = item.Id }, item);
            return result;
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Position item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            Position found = _items.Find(i => i.Id == id);
            if (found == null)
            {
                return NotFound();
            }

            found.Englishname = item.Englishname;
            found.Localname = item.Localname;

            _items.Commit();

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _items.Find(i => i.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            _items.Remove(item);
            _items.Commit();
            return new NoContentResult();
        }
    }
}

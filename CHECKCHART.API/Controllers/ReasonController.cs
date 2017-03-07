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
    public class ReasonController : Controller
    {
        private IReasonRepository _items { get; set; }
        public ReasonController(IReasonRepository iReasonRepository)
        {
            _items = iReasonRepository;
        }

        [HttpGet]
        public IEnumerable<Reason> GetAll()
        {
            return _items.GetAll();
        }

        [HttpGet("{id}", Name = "GetReason")]
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
        public IActionResult Create([FromBody] Reason item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            _items.Add(item);
            _items.Commit();
            CreatedAtRouteResult result = CreatedAtRoute("GetReason", new { controller = "Reason", id = item.Id }, item);
            return result;
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Reason item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            Reason found = _items.Find(i => i.Id == id);
            if (found == null)
            {
                return NotFound();
            }

            found.Name = item.Name;

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

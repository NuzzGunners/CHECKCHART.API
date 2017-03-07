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
    public class RoleController : Controller
    {
        private IRoleRepository _items { get; set; }
        public RoleController(IRoleRepository iRoleRepository)
        {
            _items = iRoleRepository;
        }

        [HttpGet]
        public IEnumerable<Role> GetAll()
        {
            return _items.GetAll();
        }

        [HttpGet("{id}", Name = "GetRole")]
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
        public IActionResult Create([FromBody] Role item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            _items.Add(item);
            _items.Commit();
            CreatedAtRouteResult result = CreatedAtRoute("GetRole", new { controller = "Role", id = item.Id }, item);
            return result;
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Role item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            Role found = _items.Find(i => i.Id == id);
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

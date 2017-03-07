using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CHECKCHART.API.Abstract;
using CHECKCHART.API.Models;

namespace CHECKCHART.API.Controllers
{
    [Route("api/[controller]")]
    public class AuditController : Controller
    {
        public IAuditRepository _items { get; set; }
        public AuditController(IAuditRepository iAuditRepository)
        {
            _items = iAuditRepository;
        }

        [HttpGet]
        public IEnumerable<Audit> GetAll()
        {
            return _items.GetAll().Where(i => i.Cxlbydatetime == null);
        }

        [HttpGet("{an}", Name = "GetAudit")]
        public IActionResult GetById(string an)
        {
            var item = _items.GetAll().Where(i => i.An == an && i.Cxlbydatetime == null);
            return new OkObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Audit item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = Guid.NewGuid();
            item.Entrybydatetime = DateTime.Now;
            _items.Add(item);
            _items.Commit();

            CreatedAtRouteResult result = CreatedAtRoute("GetAudit", new { controller = "Audit", an = item.An }, item);
            return result;
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] Audit item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            Audit found = _items.Find(i => i.Id == id);
            if (found == null)
            {
                return NotFound();
            }

            found.Doctor = item.Doctor;
            found.Doctormaster = item.Doctormaster;
            found.Doctorconsult = item.Doctorconsult;
            found.Category = item.Category;
            found.Rwbefore = item.Rwbefore;
            found.Rwafter = item.Rwafter;
            found.Coder = item.Coder;
            found.Ward = item.Ward;
            found.Los = item.Los;
            found.Nurse = item.Nurse;
            found.Updatebyuser = item.Entrybyuser;
            found.Updatebydatetime = DateTime.Now;

            _items.Commit();

            return new NoContentResult();
        }

        [HttpPut("{id}/deleteaudit")]
        public IActionResult Delete(Guid id, [FromBody] Audit item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            Audit found = _items.Find(i => i.Id == id);
            if (found == null)
            {
                return NotFound();
            }

            found.Cxlbyuser = item.Entrybyuser;
            found.Cxlbydatetime = DateTime.Now;

            _items.Commit();

            return new NoContentResult();
        }
    }
}
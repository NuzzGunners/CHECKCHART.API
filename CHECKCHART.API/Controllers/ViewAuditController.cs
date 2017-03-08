using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CHECKCHART.API.Abstract;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CHECKCHART.API.Controllers
{
    [Route("api/[controller]")]
    public class ViewAuditController : Controller
    {
        private IViewAuditRepository _items { get; set; }
        public ViewAuditController(IViewAuditRepository iViewAuditRepository)
        {
            _items = iViewAuditRepository;
        }

        [HttpGet("{an}", Name = "GetViewAudit")]
        public IActionResult GetByAN(string AN)
        {
            var item = _items.GetAll().Where(i => i.An == AN);
            return new OkObjectResult(item);
        }
    }
}

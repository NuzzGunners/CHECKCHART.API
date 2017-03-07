using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CHECKCHART.API.Abstract;
using CHECKCHART.API.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CHECKCHART.API.Controllers
{
    [Route("api/[controller]")]
    public class AuditCategoryController : Controller
    {
        public IAuditCategoryRepository _items { get; set; }
        public AuditCategoryController(IAuditCategoryRepository iAuditCategoryRepository)
        {
            _items = iAuditCategoryRepository;
        }

        [HttpGet]
        public IActionResult GetAll(string query)
        {
            var item = _items.GetAll().Select(i => new { i.Id, i.Name }).ToArray();

            var dictionary = new Dictionary<string, object>();
            for (int i = 0; i < item.Length; i++)
            {
                dictionary.Add(string.Format("{0} : {1}", item[i].Id, item[i].Name), null);
            }

            List<dataCategory> listdataCategory = new List<dataCategory>();
            dataCategory objdataCategory = new dataCategory();
            objdataCategory.data = dictionary;
            listdataCategory.Add(objdataCategory);
            return new OkObjectResult(listdataCategory);
        }

        public class dataCategory
        {
            public object data { get; set; }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = _items.GetAll().Where(i => i.Id == id).Select(i => new { i.Id, i.Name }).ToArray();

            return new OkObjectResult(item);
        }
    }
}

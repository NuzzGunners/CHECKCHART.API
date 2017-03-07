using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CHECKCHART.API.Abstract;
using CHECKCHART.API.Models;
using CHECKCHART.API.ViewModels;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CHECKCHART.API.Controllers
{
    [Route("api/[controller]")]
    public class UserPositionController : Controller
    {
        public IUserPositionRepository _items { get; set; }
        public UserPositionController(IUserPositionRepository iUserPositionRepository)
        {
            _items = iUserPositionRepository;
        }

        [HttpGet]
        public IEnumerable<UserPositionViewModel> GetAll()
        {
            var item = from a in _items.AllIncluding(i => i.PositionNavigation, i => i.UsernameNavigation)
                       select new UserPositionViewModel
                       {
                           id = a.Id,
                           username = a.Username,
                           fullname = a.UsernameNavigation.Fullname,
                           position = a.Position,
                           positionName = a.PositionNavigation.Localname
                       };
            return item;
        }

        [HttpGet("{id}", Name = "GetUserposition")]
        public IActionResult GetById(int id)
        {
            var item = _items.Find(i => i.Id == id);

            if (item == null)
            {
                return NotFound();
            }
            return new OkObjectResult(item);
        }

        /*[HttpGet("coders")]
        public IEnumerable<UserPositionViewModel> GetCoders()
        {
            var item = from a in _items.AllIncluding(i => i.PositionNavigation, i => i.UsernameNavigation)
                       where a.Position == 56
                       select new UserPositionViewModel
                       {
                           username = a.Username,
                           fullname = a.UsernameNavigation.Fullname
                       };
            return item;
        }*/

        [HttpGet("coders")]
        public IActionResult GetCoders(string query)
        {
            var item = (from a in _items.AllIncluding(i => i.PositionNavigation, i => i.UsernameNavigation)
                       where a.Position == 56
                       select new UserPositionViewModel
                       {
                           username = a.Username,
                           fullname = a.UsernameNavigation.Fullname
                       }).ToArray();

            var dictionary = new Dictionary<string, object>();
            for (int i = 0; i < item.Length; i++)
            {
                dictionary.Add(string.Format("{0} : {1}", item[i].username, item[i].fullname), null);
            }

            List<dataCoder> listdataCoder = new List<dataCoder>();
            dataCoder objdataCoder = new dataCoder();
            objdataCoder.data = dictionary;
            listdataCoder.Add(objdataCoder);
            return new OkObjectResult(listdataCoder);
        }

        public class dataCoder
        {
            public object data { get; set; }
        }

        [HttpGet("{username}/position")]
        public IEnumerable<UserPositionViewModel> GetUserpositionLogin(string username)
        {
            var item = from a in _items.AllIncluding(i => i.UsernameNavigation,i => i.PositionNavigation)
                       where a.Username.ToLower() == username.ToLower()
                       orderby a.PositionNavigation.PositionOrder
                       select new UserPositionViewModel
                       {
                           id = a.Id,
                           username = a.Username,
                           fullname = a.UsernameNavigation.Fullname,
                           position = a.Position,
                           positionName = a.PositionNavigation.Englishname,
                           positionLocalName = a.PositionNavigation.Localname
                       }; 
            return item;
        }

        [HttpGet("{position}/sendtouser")]
        public IEnumerable<UserPositionViewModel> GetSendToUser(string position)
        {
            string sendtouserpositon = "";
            string sendtouserpositon2 = "";

            if (position == "Ward")
            {
                sendtouserpositon = "Medical Record Receive Chart";
            }
            else if (position == "Medical Record Receive Chart")
            {
                sendtouserpositon = "Coder";
            }
            else if (position == "Audit")
            {
                sendtouserpositon = "Coder";
            }
            else if (position == "Coder")
            {
                sendtouserpositon = "Diag";
            }
            else if (position == "Diag")
            {
                sendtouserpositon = "Medical Record Check Chart";
            }
            else if (position == "Medical Record Check Chart")
            {
                sendtouserpositon = "Scan";
            }
            else if (position == "Scan")
            {
                sendtouserpositon = "Revenue Keep Center Check Chart";
            }
            else if (position == "Revenue Keep Center Check Chart")
            {
                sendtouserpositon = "Revenue Keep Center";
            }
            else if (position == "Revenue Keep Center")
            {
                sendtouserpositon = "Locker";
            }

            var item = from a in _items.AllIncluding(i => i.UsernameNavigation, i => i.PositionNavigation)
                           where (sendtouserpositon2 == "" && a.PositionNavigation.Englishname == sendtouserpositon) ||
                           (sendtouserpositon2 != "" && (a.PositionNavigation.Englishname == sendtouserpositon || a.PositionNavigation.Englishname == sendtouserpositon2))
                       select new UserPositionViewModel
                       {
                           username = a.Username,
                           fullname = a.UsernameNavigation.Fullname,
                           positionName = a.PositionNavigation.Englishname
                       };
            return item;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Userposition item)
        {
            if (item == null)
            {
                return BadRequest();
            }
   
            bool isAlreadyItem = _items.GetAll().Any(c => c.Username == item.Username && c.Position == item.Position);
            if (!isAlreadyItem)
            {
                _items.Add(item);
            }
            _items.Commit();

            return CreatedAtRoute("GetUserposition", new { Controller = "Userposition", id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Userposition item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            Userposition found = _items.Find(i => i.Id == id);
            if (found == null)
            {
                return NotFound();
            }

            found.Position = item.Position;

            _items.Commit();

            return CreatedAtRoute("GetUserposition", new { Controller = "Userposition", id = item.Username }, item);
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

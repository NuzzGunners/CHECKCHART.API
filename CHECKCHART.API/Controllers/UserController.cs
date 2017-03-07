using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CHECKCHART.API.Models;
using Microsoft.EntityFrameworkCore;
using CHECKCHART.API.Abstract;
using CHECKCHART.API.ViewModels;

namespace CHECKCHART.API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        public IUserRepository _items { get; set; }
        public UserController(IUserRepository iUserRepository)
        {
            _items = iUserRepository;
        }

        [HttpGet]
        public IEnumerable<UserViewModel> GetAll()
        {
            var item = from a in _items.AllIncluding(i => i.Userposition, i => i.RoleuserNavigation)
                       select new UserViewModel
                       {
                           Username = a.Username,
                           Fullname = a.Fullname,
                           roleuserName = a.RoleuserNavigation.Name
                           //positionName = a.Userposition.
                       };
            return item;
        }

        /*
         [HttpGet("{position}/sendtouser")]
        public IEnumerable<UserViewModel> GetSendToUser(string position)
        {
            string sendtouserpositon = "";
            string sendtouserpositon2 = "";

            if (position == "Auditor")
            {
                sendtouserpositon = "Staff";
            }
            else if (position == "Staff")
            {
                sendtouserpositon = "Medical Record Receive Chart";
            }
            else if (position == "Medical Record Receive Chart")
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

            var item = from a in _items.AllIncluding(i => i.RoleuserNavigation, i => i.Userposition)
                           //where (sendtouserpositon2 == "" && a.Userposition == sendtouserpositon) ||
                           //(sendtouserpositon2 != "" && (a.PositionNavigation.Name == sendtouserpositon || a.PositionNavigation.Name == sendtouserpositon2))
                       select new UserViewModel
                       {
                           Username = a.Username,
                           Fullname = a.Fullname,
                           roleuserName = a.RoleuserNavigation.Name,
                           position = a.Userposition
                       };
            return item;
        }
             */

        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult GetById(string id)
        {
            var item = from a in _items.AllIncluding(i => i.RoleuserNavigation, i => i.Userposition)
                       where a.Username.ToLower() == id.ToLower()
                       select new
                       {
                           a.Username,
                           a.Fullname,
                           Password = UserPasswordProfile.Decrypt(a.Password),
                           passwordConfirm = UserPasswordProfile.Decrypt(a.Password),
                           a.Roleuser,
                           RoleuserName = a.RoleuserNavigation.Name
                       };
            if (item == null)
            {
                return NotFound();
            }
            return new OkObjectResult(item);
        }

        [HttpGet("{query}/search")]
        public IActionResult GetSearch(string query)
        {
            var item = _items.GetAll().Where(i => i.Username.ToLower().Contains(query.ToLower()) || i.Fullname.ToLower().Contains(query.ToLower()))
                .Select(i => new { i.Username, i.Fullname }).Take(10).ToArray();

            var dictionary = new Dictionary<string, object>();
            for (int i = 0; i < item.Length; i++)
            {
                dictionary.Add(item[i].Username, null);
            }

            List<dataUser> listdataUser = new List<dataUser>();
            dataUser objdataUser = new dataUser();
            objdataUser.data = dictionary;
            listdataUser.Add(objdataUser);
            return new OkObjectResult(item);
        }
        public class dataUser
        {
            public object data { get; set; }
        }

        [HttpPost]
        public IActionResult Create([FromBody] User item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Password = UserPasswordProfile.Encrypt(item.Password);
            _items.Add(item);
            _items.Commit();

            var newitem = from a in _items.AllIncluding(i => i.RoleuserNavigation)
                          where a.Username == item.Username
                          select new
                          {
                              a.Username,
                              a.Fullname,
                              roleuserName = a.RoleuserNavigation.Name
                          };
            return CreatedAtRoute("GetUser", new { Controller = "User", id = item.Username }, newitem);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] User item)
        {
            if (item == null || item.Username != id)
            {
                return BadRequest();
            }

            User found = _items.Find(i => i.Username == id);
            if (found == null)
            {
                return NotFound();
            }

            found.Password = UserPasswordProfile.Encrypt(item.Password); ;
            found.Fullname = item.Fullname;
            //found.Position = item.Position;
            found.Roleuser = item.Roleuser;

            _items.Commit();

            var newitem = from a in _items.AllIncluding(i => i.RoleuserNavigation)
                          where a.Username == item.Username
                          select new
                          {
                              a.Username,
                              a.Fullname,
                              roleuserName = a.RoleuserNavigation.Name
                          };
            return CreatedAtRoute("GetUser", new { Controller = "User", id = item.Username }, newitem);
        }

        [HttpPut("{username}/updatepassword")]
        public IActionResult UpdatePassword(string username, [FromBody] User item)
        {
            if (item == null || item.Username != username)
            {
                return BadRequest();
            }

            var ec = UserPasswordProfile.Encrypt(item.Password);
            var dc = UserPasswordProfile.Decrypt(ec);

            User found = _items.Find(i => i.Username == username);
            if (found == null)
            {
                return NotFound();
            }

            found.Password = UserPasswordProfile.Encrypt(item.Password);

            _items.Commit();

            var newitem = from a in _items.AllIncluding(i => i.RoleuserNavigation)
                          where a.Username == item.Username
                          select new
                          {
                              a.Username,
                              a.Fullname,
                              roleuserName = a.RoleuserNavigation.Name
                          };
            return CreatedAtRoute("GetUser", new { Controller = "User", id = item.Username }, newitem);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var item = _items.Find(i => i.Username == id);

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

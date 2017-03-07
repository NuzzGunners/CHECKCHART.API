using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CHECKCHART.API.Models;
using CHECKCHART.API.ViewModels;
using CHECKCHART.API.Abstract;

namespace CHECKCHART.API.Controllers
{
    [Route("api/[controller]")]
    public class CheckchartController : Controller
    {
        private ICheckchartRepository _items { get; set; }
        public CheckchartController(ICheckchartRepository iCheckchartRepository)
        {
            _items = iCheckchartRepository;
        }

        [HttpGet]
        public IEnumerable<Checkchart> GetAll()
        {
            return _items.GetAll().Where(i => i.Cxlbyuserdatetime == null);
        }

        [HttpGet("{an}", Name = "GetCheckchart")]
        public IActionResult getPatientCheckchart(string an)
        {
            var item = from a in _items.AllIncluding(i => i.ReceivebyuserNavigation,
                i => i.ReceivebypositionNavigation,
                i => i.SendtouserNavigation,
                i => i.SendtopositionNavigation,
                i => i.SendtobyuserNavigation)
                       where a.An == an
                       && a.Cxlbyuserdatetime == null
                       orderby a.Id descending
                       select new
                       {
                           id = a.Id,
                           an = a.An,
                           receivebyuser = a.Receivebyuser,
                           receivebydatetime = a.Receivebydatetime,
                           receivebyposition = a.Receivebyposition,
                           receivebypositionname = a.ReceivebypositionNavigation.Englishname,
                           receivebyusername = a.ReceivebyuserNavigation.Fullname,
                           sendtouser = a.Sendtouser,
                           sendtodatetime = a.Sendtodatetime,
                           sendtoposition = a.Sendtoposition,
                           sendtopositionname = a.SendtopositionNavigation == null ? null : a.SendtopositionNavigation.Englishname,
                           sendtobyusername = a.SendtobyuserNavigation == null ? null : a.SendtobyuserNavigation.Fullname
                       };

            if (item == null)
            {
                return NotFound();
            }
            return new OkObjectResult(item);
        }

        [HttpGet("{an}/last")]
        public IActionResult getLastPatientCheckchart(string an)
        {
            var item = _items.GetAll()
                .Where(i => i.An == an && i.Cxlbyuserdatetime == null)
                .Select(i => new
                {
                    i.Id,
                    i.Receivebyposition,
                    i.Sendtoposition,
                    i.Sendtouser
                }).LastOrDefault();
                
            return new OkObjectResult(item != null ? item : new object());
        }

        [HttpGet("{an}/position/{Receivebyposition}")]
        public IActionResult getPatientCheckchartByReceive(string an, int Receivebyposition)
        {
            var item = _items.GetAll().Where(i => i.An == an
                                        && i.Receivebyposition == Receivebyposition
                                        && i.Cxlbyuserdatetime == null).OrderByDescending(i => i.Id);
            if (item == null)
            {
                return NotFound();
            }
            return new OkObjectResult(item);
        }

        [HttpGet("{an}/log")]
        public IEnumerable<CheckchartLogViewModel> getPatientCheckchartLog(string an)
        {
            var item = from a in _items.AllIncluding(i => i.ReceivebypositionNavigation,
                                                        i => i.SendtopositionNavigation,
                                                        i => i.ReceivebyuserNavigation,
                                                        i => i.SendtobyuserNavigation,
                                                        i => i.SendtouserNavigation)
                       where a.An == an
                       && a.Cxlbyuserdatetime == null
                       select new CheckchartLogViewModel
                       {
                           id = a.Id,
                           an = a.An,
                           receivebyuser = a.Receivebyuser,
                           receivebyusername = a.ReceivebyuserNavigation.Fullname,
                           receivebydatetime = a.Receivebydatetime,
                           receivebyposition = a.ReceivebypositionNavigation.Englishname,
                           receivebypositionlocalname = a.ReceivebypositionNavigation.Localname,
                           sendtobyuser = a.Sendtobyuser,
                           sendtobyusername = a.SendtobyuserNavigation == null ? null : a.SendtobyuserNavigation.Fullname,
                           sendtodatetime = a.Sendtodatetime,
                           sendtoposition = a.SendtopositionNavigation == null ? null : a.SendtopositionNavigation.Englishname,
                           sendtopositionlocalname = a.SendtopositionNavigation == null ? null : a.SendtopositionNavigation.Localname,
                           sendtouser = a.Sendtouser,
                           sendtousername = a.SendtouserNavigation == null ? null : a.SendtouserNavigation.Fullname
                       };
            return item;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Checkchart item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            int isAlreadyItem = _items.GetAll().Count(c => c.An == item.An && c.Receivebyposition == item.Receivebyposition && c.Cxlbyuserdatetime == null);
            if (isAlreadyItem == 0)
            {
                item.Receivebydatetime = DateTime.Now;
                _items.Add(item);
                _items.Commit();
            }
            CreatedAtRouteResult result = CreatedAtRoute("GetCheckchart", new { controller = "Checkchart", an = item.An }, item);
            return result;
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Checkchart item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            Checkchart found = _items.Find(i => i.Id == id);
            if (found == null)
            {
                return NotFound();
            }

            found.Sendtouser = item.Sendtouser;
            found.Sendtoposition = item.Sendtoposition;
            found.Sendtodatetime = DateTime.Now;
            found.Sendtobyuser = item.Sendtobyuser;


            _items.Commit();

            return new NoContentResult();
        }

        [HttpPost("addmultiple/")]
        public IActionResult CreateMultiple([FromBody] List<Checkchart> item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            foreach (var i in item)
            {
                i.Receivebydatetime = DateTime.Now;
                _items.Add(i);

                _items.Commit();
            }
            return new NoContentResult();
        }

        [HttpPut("updatemultiple/")]
        public IActionResult UpdateMultiple([FromBody] List<Checkchart> item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            foreach (var i in item)
            {
                Checkchart found = _items.Find(f => f.Id == i.Id);

                if (found == null)
                {
                    return NotFound();
                }

                found.Sendtouser = i.Sendtouser;
                found.Sendtoposition = i.Sendtoposition;
                found.Sendtodatetime = DateTime.Now;
                found.Sendtobyuser = i.Sendtobyuser;

                _items.Commit();
            }

            return new NoContentResult();
        }

        [HttpPut("{id}/deletecheckchart")]
        public IActionResult Delete(int id, [FromBody] Checkchart item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            Checkchart found = _items.Find(i => i.Id == id);

            if (found == null)
            {
                return NotFound();
            }

            if (found.Sendtodatetime != null)
            {
                found.Sendtouser = null;
                found.Sendtobyuser = null;
                found.Sendtoposition = null;
                found.Sendtodatetime = null;
            }
            else
            {
                found.Cxlbyuser = item.Cxlbyuser;
                found.Cxlbyuserdatetime = DateTime.Now;
                found.Cxlbyuserreason = item.Cxlbyuserreason;
            }

            _items.Commit();

            return new NoContentResult();
        }
    }
}

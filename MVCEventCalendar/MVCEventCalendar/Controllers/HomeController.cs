using MVCEventCalendar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCEventCalendar.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetEvents()
        {
            using (OneBaseEntities2 dc = new OneBaseEntities2())
            {
                var events = dc.tbl_HR_PTO_Calendar.ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [HttpPost]
        public JsonResult SaveEvent(tbl_HR_PTO_Calendar e)
        {
            var status = false;
            using (OneBaseEntities2 dc = new OneBaseEntities2())
            {
                if (e.ID > 0)
                {
                    //Update the event
                    var v = dc.tbl_HR_PTO_Calendar.Where(a => a.ID == e.ID).FirstOrDefault();
                    if (v != null)
                    {
                        v.Name = e.Name;
                        v.Start = e.Start;
                        v.End = e.End;
                        v.Description = e.Description;
                        v.IsFullDay = e.IsFullDay;
                        v.ThemeColor = e.ThemeColor;
                    }
                }
                else
                {
                    dc.tbl_HR_PTO_Calendar.Add(e);
                }
                dc.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult DeleteEvent(int ID)
        {
            var status = false;
            using (OneBaseEntities2 dc = new OneBaseEntities2())
            {
                var v = dc.tbl_HR_PTO_Calendar.Where(a => a.ID == ID).FirstOrDefault();
                if (v != null)
                {
                    dc.tbl_HR_PTO_Calendar.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}
using MVCEventCalendar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCEventCalendar.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Autherize(MVCEventCalendar.Models.tbl_User userModel)
        {
            using (OneBaseEntities1 db = new OneBaseEntities1())
            {
                var userDetails = db.tbl_User.Where(x => x.EmployeeNo == userModel.EmployeeNo && x.Password == userModel.Password).FirstOrDefault();
                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Wrong username or password";
                    return View("Index", userModel);
                }
                else
                {
                    Session["UserId"] = userDetails.UserId;
                    Session["EmployeeNo"] = userDetails.EmployeeNo;
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        public ActionResult Logout()
        {
            //int userId = (int)Session["UserId"];
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}
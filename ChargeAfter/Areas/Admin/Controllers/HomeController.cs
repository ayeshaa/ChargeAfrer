using ChargeAfter.Models;
using ChargeAfter.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChargeAfter.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        ChargeAfterEntities db;
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            var username = form["username"];
            var password = form["password"];
            if(username== "Admin" && password == "Admin")
            {
                using (db = new ChargeAfterEntities())
                {
                    var today = DateTime.Now.AddDays(-7);
                    var query = (from u in db.Users
                                 join r in db.Requests
                                 on u.Id equals r.UserID into bases
                                 from sb in bases.DefaultIfEmpty()
                                 join ro in db.RequestOffers
                                 on sb.UserID equals ro.UserID into ess
                                 from es in ess.DefaultIfEmpty()
                                 select new TBL
                                 {
                                     Id = u.Id,
                                     Mobile = u.Mobile,
                                     Phone = u.Phone,
                                     FirstName = u.FirstName,
                                     Email = u.Email,
                                     lender = es.Lender,
                                     //ReqID = (int)u.Id,
                                     Amount = es.ApprovedAmmount,
                                     rDate = (DateTime?)sb.RequestDate ?? DateTime.Now,
                                     Status = sb.RequestStatus
                                 }).Where(x => DbFunctions.TruncateTime(x.rDate) >= today).OrderByDescending(x => x.Id);
                    //var last = query.Last();
                    //ViewData["last"] = last;
                    return View("DashBoard", query.ToList());
                }
            }
            else
            {
                ViewData["messsage"] = "incorrect user name or password";
                return View();
            }
        }
        public ActionResult DashBoard()
        {
            using (db = new ChargeAfterEntities())
            {
                var today = DateTime.Now.AddDays(-7);
                var query = (from u in db.Users
                             join r in db.Requests
                             on u.Id equals r.UserID into bases
                             from sb in bases.DefaultIfEmpty()
                             join ro in db.RequestOffers
                             on sb.UserID equals ro.UserID into ess
                             from es in ess.DefaultIfEmpty()
                             select new TBL
                             {
                                 Id = u.Id,
                                 Mobile = u.Mobile,
                                 Phone = u.Phone,
                                 FirstName = u.FirstName,
                                 Email = u.Email,
                                 lender = es.Lender,
                                 //ReqID = (int)u.Id,
                                 Amount = es.ApprovedAmmount,
                                 rDate = (DateTime?)sb.RequestDate ?? DateTime.Now,
                                 Status = sb.RequestStatus
                             }).Where(x => DbFunctions.TruncateTime(x.rDate) >= today).OrderByDescending(x => x.Id);
                //var last = query.LastOrDefault();
                //ViewData["last"] = last;
                return View(query.ToList());
            }
        }
    }
}
using ChargeAfter.Models;
using ChargeAfter.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChargeAfter.Areas.Admin.Controllers
{
    public class FinancialSettlementsController : Controller
    {
        ChargeAfterEntities db;
        public ActionResult Index()
        {
            using (db = new ChargeAfterEntities())
            {
                var today = DateTime.Now;
                var query = (from u in db.Users
                             join r in db.Requests
                             on u.Id equals r.UserID //into bases
                             //from sb in bases.DefaultIfEmpty()
                             join ro in db.RequestOffers
                             on r.UserID equals ro.UserID // into ess
                             //from es in ess.DefaultIfEmpty()
                             //where sb.RequestDate == today.Date
                             select new TBL
                             {
                                 Id = u.Id,
                                 //DOB = u.DOB.Value,
                                 Mobile = u.Mobile,
                                 Phone = u.Phone,
                                 FirstName = u.FirstName,
                                 //LastName = u.LastName,
                                 Email = u.Email,
                                 //Username = u.FirstName,
                                 lender = ro.Lender,
                                 //ReqID = (int)u.Id,
                                 Amount = ro.ApprovedAmmount,
                                 Date = (DateTime?)r.RequestDate ?? DateTime.Now,
                                 Status = r.RequestStatus

                             }).ToList().Where(x => x.Date.Value.Date > today.AddDays(-7));

                //var yestreday = DateTime.Now.AddDays(-1); 
                //float yesterdaysSettlement = 0;
                //foreach(var item in query)
                //{
                //    var rDate = item.Date.Value.Date; 
                //    var result = DateTime.Compare(rDate.Date, yestreday.Date);
                //    if (result == 0)
                //    {
                //        yesterdaysSettlement = yesterdaysSettlement + Convert.ToSingle(item.Amount);
                //    }
                //}
                

                return View(query);
            }
        }
    }
}
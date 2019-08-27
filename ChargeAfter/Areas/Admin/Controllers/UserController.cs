using ChargeAfter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChargeAfter.ViewModel;
using System.Data.Entity;

namespace ChargeAfter.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        ChargeAfterEntities db;
        public ActionResult Index()
        {
            using (db = new ChargeAfterEntities())
            {
                             var query = (from u in db.Users
                                          from ua in db.UserAdresses.Where(c => c.UserId == u.Id).DefaultIfEmpty()
                                          from r in db.Requests.Where(f => f.UserID == u.Id).DefaultIfEmpty()
                                          from ro in db.RequestOffers.Where(comp => comp.UserID == u.Id).DefaultIfEmpty()
                                          select new TBL
                             {
                                 Id = u.Id,
                                 ReqId = r.ReqID,
                                 Mobile = u.Mobile,
                                 Phone = u.Phone,
                                 FirstName = u.FirstName,
                                 //LastName = u.LastName,
                                 Email = u.Email,
                                 //Username = u.FirstName,
                                 lender =  ro.Lender,
                                 //ReqID = (int)u.Id,
                                 Amount = ro.ApprovedAmmount,
                                 rDate = r.RequestDate ?? DateTime.Now,
                                 Status = r.RequestStatus

                             }).OrderByDescending(x => x.Id );

                return View(query.ToList());
            }
        }

        public ActionResult Details(int id)
        {
            using (db = new ChargeAfterEntities())
            {
                var query = (from u in db.Users
                             from ua in db.UserAdresses.Where(c => c.UserId == u.Id).DefaultIfEmpty()
                             from r in db.Requests.Where(f => f.UserID == u.Id).DefaultIfEmpty()
                             from ro in db.RequestOffers.Where(comp => comp.UserID == u.Id).DefaultIfEmpty()
                             where u.Id == id
                             select new TBL
                             {
                                 Id = u.Id,
                                 ReqId = r.ReqID,
                                 Mobile = u.Mobile,
                                 Phone = u.Phone,
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                                 address = ua.Adress,
                                 city = ua.City,
                                 country = ua.Country,
                                 zip = ua.PostalCode,
                                 state = ua.State,
                                 Email = u.Email,
                                 Username = u.FirstName,
                                 lender = ro.Lender,
                                 EmpStatus = u.EmploymentStatus,
                                 HousingStatus = u.HousingStatus,

                                 ApprovedAmmount =  ro.ApprovedAmmount,
                                 MonthlyAmount = ro.MonthlyAmount,
                                 MonthCount = ro.MonthCount,
                                 IncreasePercent = ro.IncreasePercent,
                                 //ReqID = int.Parse(ro.ReqID.ToString()),
                                 TotalPayback =ro.TotalPayback,
                                 
                                 socialSecurity = u.SocialSecurityNumber,
                                 YearsAtResidance = u.YearatResidence,
                                 monthlyRent = u.monthlyRent,
                                 DriverLicense = u.DriverLicense,
                                 DRiveState = u.DriverLicenseState,
                                 
                                 GrossIndividual = u.GrossAnnualIndividual,
                                 NetIncom = u.NetMonthlyIncome,
                                 Amount = ro.ApprovedAmmount,
                                // Date = r.RequestDate.Value,
                                 Status = r.RequestStatus,
                                 SalaryFrequency = u.SalaryFrequency,
                             }).FirstOrDefault();

                return Json(new { success = true, query }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Refund(FormCollection form)
        {
            try
            { 
            var id = form["rid"];
            int uid = Convert.ToInt32(id);
            using (db = new ChargeAfterEntities())
            {
                var reqeust = (from r in db.Requests where r.UserID == uid select r).FirstOrDefault();
                long rid = reqeust.ReqID;
                Request model = db.Requests.Find(rid);
                model.RequestStatus = "Refunded";
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                    TempData["message"] = "You successfully Refunded this amount.";
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["error"] = "Sorry we can not process the request !!";
                return RedirectToAction("Index");
            }
        }
    }
}
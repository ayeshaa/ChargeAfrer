using ChargeAfter.Models;
using ChargeAfter.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ChargeAfter.Controllers
{
    public class HomeController : Controller
    {
        ChargeAfterEntities db;
        
        private HttpContextBase Context { get; set; }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ChargeAfterFinancingFlow()
        {
            return View();
        }
        public ActionResult ThankYou()
        {
            return View();
        }
        public ActionResult Computer()
        {
            return View();
        }
        public ActionResult Laptop()
        {
            return View();
        }
        public ActionResult GamingEditing()
        {
            using (db = new ChargeAfterEntities())
            {
                var product = (from p in db.Products where p.Name.Contains("Dell 15.6") select p).FirstOrDefault();
                return View(product);
            }
        }


        public ActionResult Product()
        {
            using (db = new ChargeAfterEntities())
            {
                var product = (from p in db.Products select p).ToList();
                return View(product);
            }
        }
        public ActionResult CheckOut()
        {
            return View();
        }
        public ActionResult BeginCheckout()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CheckOut(FormCollection form)
        {
            using (db = new ChargeAfterEntities())
            {
                try
                {
                    ViewData["Awesome"] = "Awesome";
                    Session["Name"] = form["firstName"] + " " + form["lastName"];
                    Session["email"] = form["email"];
                    Session["phone"] = form["phone"];
                    Session["country"] = form["country"];
                    Session["address"] = form["address1"] + form["address2"] + form["address3"];
                    Session["City"]= form["city"];
                    Session["state"] = form["state"];
                    Session["Zip"] = form["zip"];
                    Session["country"] = form["country"];
                    TempData["Name"] = form["firstName"] + " " + form["lastName"];
                    TempData["phone"] = form["phone"];
                    TempData["address"] = form["address1"] + form["address2"] + form["address3"];
                    TempData["city"] = form["city"];
                    TempData["state"] = form["state"];
                    TempData["Zip"] = form["zip"];
                    TempData["country"] = form["country"];
                    return View("ConfirmShipping");
                }
                catch(Exception ex)
                {
                    ViewData["Awesome"] = "Oops " + ex.Message;
                    return View();

                }
                
           
            }
        }
        public ActionResult ConfirmShipping()
        {
                TempData["name"] = TempData["name"];
                TempData["address"] = TempData["address"];
                TempData["city"] = TempData["city"];
                TempData["state"] = TempData["state"];
                TempData["Zip"] = TempData["Zip"];
                TempData["country"] = TempData["country"];
                return View();
           
        }
        public ActionResult Payment()
        {
                TempData["name"] = Session["Name"];
                TempData["email"] = Session["email"];
                TempData["phone"] = Session["phone"];
                TempData["address"] = Session["address"];
                TempData["city"] = Session["City"];
                TempData["state"] = Session["state"];
            TempData["Zip"] = Session["Zip"];
                TempData["country"] = Session["country"];
           
                return View();
        }
        [HttpPost]
        public ActionResult Payment(string Name, string Uemail, string phone, string dateBirth, string SSN,
           string EmplStatus, string estimatedCredit, string housingStatus, string NetMonthlySalary, string GrossIn,
           string SalaryFrequency, string Address, string State, string City, string monthRent, string driverLicenseNum,
           string driversState, string driversDate, string yearsAtResidance, FormCollection form)
        {
            try
            {
                using (db = new ChargeAfterEntities())
                {
                    var user = new User();
                    var userAddress = new UserAdress();
                    var req = new Request();
                    var name = Name;
                    var email = Uemail;
                    user.FirstName = Name;
                    user.Mobile = phone;
                    user.Email = Uemail;
                    if(dateBirth !=null)
                    { 
                    user.DOB = Convert.ToDateTime(dateBirth);
                    }
                    user.SocialSecurityNumber = SSN;
                    user.EstimatedCreditRange = estimatedCredit;
                    user.EmploymentStatus = EmplStatus;
                    user.HousingStatus = housingStatus;
                    user.GrossAnnualIndividual = GrossIn;
                    user.NetMonthlyIncome = NetMonthlySalary;
                    user.SalaryFrequency = SalaryFrequency;
                    user.YearatResidence = yearsAtResidance;
                    user.monthlyRent = monthRent;
                    user.DriverLicense = driverLicenseNum;
                    user.DriverLicenseState = driversState;
                    if(driversDate.Length > 1)
                    { 
                    user.DriverLicensExpirationDate = Convert.ToDateTime(driversDate);
                    }
                    db.Users.Add(user);
                    db.SaveChanges();

                    userAddress.UserId = user.Id;
                    userAddress.Country = Session["country"].ToString();
                    userAddress.Adress = Address;
                    userAddress.City = City;
                    userAddress.State = State;
                    userAddress.PostalCode = Session["Zip"].ToString();
                    db.UserAdresses.Add(userAddress);

                    var items = (List<Item>)Session["cart"];
                    float subTot = 0;
                    float total = 0;
                    if (items != null)
                    {
                        foreach (Item itemPro in (List<Item>)Session["cart"])
                        {
                            float price = Convert.ToSingle(@itemPro.Pro.Price) * 1;
                            subTot = subTot + price;
                        }
                        total = subTot + 100;
                    }
                    decimal amount = Math.Round((decimal)total, 0);

                    req.UserID = user.Id;
                    req.RequestDate = DateTime.Now;
                    req.RequestAmount = amount;
                    req.RequestStatus = "Pending";
                    TempData["id"] = user.Id;
                    Session["idU"] = user.Id;
                    int userId = user.Id;
                    db.Requests.Add(req);
                    db.SaveChanges();

                    ViewData["message"] = "Sucessful";
                    return Json(userId, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ViewData["message"] = "Sorry " + ex.Message;
                return View();
            }
        }
        public ActionResult ConfirmPayment()
        {
                TempData["name"] = Session["chargeAName"];
                TempData["mobile"] = Session["ChargAmobile"];
                TempData["email"] = Session["ChargAemail"];
                TempData["DOB"] = Session["ChargADOB"];
                return View();
        }
        [HttpPost]
       
        public ActionResult Approved()
        {
                return View();
        }
        public ActionResult Offers()
        {
            TempData["id"] = Session["idU"];
            return View();
        }
        
        public ActionResult OfferFortiva(int id)
        {
            using (db = new ChargeAfterEntities())
            {
                try
                {
                    var offer = new RequestOffer();
                    var item = (List<Item>)Session["cart"];
                    float subTot = 0;
                    float pay = 0;
                    decimal approvedAmount = 0;
                    if (item != null)
                    {
                        foreach (Item itemPro in (List<Item>)Session["cart"])
                        {
                            float price = Convert.ToSingle(@itemPro.Pro.Price) * 1;
                            subTot = subTot + price;
                            Session["Subtotal"] = subTot;

                        }

                        pay = Convert.ToSingle(100 + subTot);
                        approvedAmount = Math.Round((decimal)pay, 0);
                    }
                    decimal monthly = approvedAmount / 6;
                    decimal monthlyPay = Math.Round((decimal)monthly, 0);
                    int? intIdt = db.Users.Max(u => (int?)u.Id);
                    offer.UserID = intIdt;

                    offer.TotalPayback = 6;
                    offer.IncreasePercent = 0;
                    offer.ApprovedAmmount = approvedAmount;
                    offer.MonthlyAmount = monthlyPay;
                    offer.Lender = "Fortiva";
                    offer.MonthCount = 6;

                    db.RequestOffers.Add(offer);
                    var req = (from r in db.Requests where r.UserID == intIdt select r).FirstOrDefault();
                    long reqID = req.ReqID;
                    Request model = db.Requests.Find(reqID);
                    model.RequestStatus = "Authorized";
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["PayBack"] = "6";
                    Session["PayBack"] = "6";
                    return RedirectToAction("ReviewSubmit");
             }
                catch (Exception ex)
                {
                    TempData["message"] = "We can not proceed your request right now !!! Fill all the fields carefully and try again";
                    return RedirectToAction("Payment");
                }
            }

        }
        public ActionResult OfferProsper(int id)
        {
            using (db = new ChargeAfterEntities())
            {
                try
                {
                    var offer = new RequestOffer();
                    var item = (List<Item>)Session["cart"];
                    float subTot = 0;
                    float pay = 0;
                    decimal approvedAmount = 0;
                    if (item != null)
                    {
                        foreach (Item itemPro in (List<Item>)Session["cart"])
                        {
                            float price = Convert.ToSingle(@itemPro.Pro.Price) * 1;
                            subTot = subTot + price;
                            Session["Subtotal"] = subTot;

                        }

                        pay = Convert.ToSingle(100 + subTot);
                        approvedAmount = Math.Round((decimal)pay, 0);
                    }
                    decimal monthly = approvedAmount / 12;
                    decimal monthlyPay = Math.Round((decimal)monthly, 0);
                    int? intIdt = db.Users.Max(u => (int?)u.Id);
                    offer.UserID = intIdt;
                    offer.TotalPayback = 12;
                    offer.IncreasePercent = 0;
                    offer.ApprovedAmmount = approvedAmount;
                    offer.MonthlyAmount = monthlyPay;
                    offer.Lender = "Prosper";
                    offer.MonthCount = 12;
                    db.RequestOffers.Add(offer);
                    var req = (from r in db.Requests where r.UserID == intIdt select r).FirstOrDefault();
                    long reqID = req.ReqID;
                    Request model = db.Requests.Find(reqID);
                    model.RequestStatus = "Authorized";
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["PayBack"] = null;
                    Session["PayBack"] = null;
                    return RedirectToAction("ReviewSubmit");
                }
        
            catch (Exception ex)
            {
                    TempData["message"] = "We can not proceed your request right now !!! Fill all the fields carefully and try again";
                    return RedirectToAction("Payment");
                }
            }
        }

        private int isExisting(int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Pro.Id == id)
                    return i;
            return -1;
        }
       
        public ActionResult ReviewSubmit()
        {
            return View();
        }
        public ActionResult Delete(int id)
        {
            int index = isExisting(id);
            List<Item> cart = (List<Item>)Session["cart"];
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return View("Cart");
        }
        public ActionResult Cart()
        {
            return View();
        }
        public ActionResult AddToCart(int id)
        {
            if (Session["cart"] == null)
            {
                using (db = new ChargeAfterEntities())
                {
                    List<Item> cart = new List<Item>();
                    var data = (from d in db.Products where d.Id == id select d).FirstOrDefault();
                    cart.Add(new Item(data, 1));
                    Session["cart"] = cart;
                }
            }
            else
            {
                using (db = new ChargeAfterEntities())
                {
                    List<Item> cart = (List<Item>)Session["cart"];
                    int index = isExisting(id);
                    if (index == -1)
                        cart.Add(new Item(db.Products.Find(id), 1));
                    else
                        cart[index].Quantity++;
                    Session["cart"] = cart;
                }
            }
            TempData["Cart"] = "cart is not empty";
            //ViewData["AddedCart"] = "cart is not empty";
            return Redirect(Request.UrlReferrer.PathAndQuery);
            
            //using (db = new ChargeAfterEntities())
            //{
            //    var product = (from p in db.Products select p).ToList();
            //    return View("Product", product);
            //}
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
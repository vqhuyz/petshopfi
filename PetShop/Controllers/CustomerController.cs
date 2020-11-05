using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Facebook;
using PetShop.Models;

namespace PetShop.Controllers
{
    public class CustomerController : Controller
    {
        PetShopDataContext db = new PetShopDataContext();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListOrderCustomer(long id)
        {
            var lstOrder = db.Orders.Where(x => x.CustomerId == id).ToList();      
            return View(lstOrder);
        }

        public ActionResult OrderDetailsCustomer(long id)
        {
            var lstOrderDetails = db.OrderDetails.Where(x => x.OrderId == id).ToList();
            return PartialView(lstOrderDetails);
        }

        private Uri RediredtUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);

                uriBuilder.Query = null;

                uriBuilder.Fragment = null;

                uriBuilder.Path = Url.Action("FacebookCallback");

                return uriBuilder.Uri;

            }

        }


        [AllowAnonymous]
        public ActionResult Facebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = "1203041996744003",

                client_secret = "faa4be3a07d117049e2725c7deafe643",

                redirect_uri = RediredtUri.AbsoluteUri,

                response_type = "code",

                scope = "email"
            });
            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var cus = new Customer();
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {

                client_id = "1203041996744003",

                client_secret = "faa4be3a07d117049e2725c7deafe643",

                redirect_uri = RediredtUri.AbsoluteUri,

                code = code
            });
            var accessToken = result.access_token;
            Session["AccessToken"] = accessToken;
            fb.AccessToken = accessToken;
            dynamic me = fb.Get("me?fields=link,first_name,last_name,email,gender");

            string email = me.email;


            cus.FullName = me.first_name + "" + me.last_name;
            cus.Email = me.email;
            cus.Username = me.email;
            cus.Status = true;


            var insert = db.Customers.SingleOrDefault(x => x.Username == email);
            if (insert == null)
            {
                db.Customers.InsertOnSubmit(cus);
                db.SubmitChanges();
                Session["UserLogin"] = cus;
            }
            else
            {
                Session["UserLogin"] = cus;
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public JsonResult Login(string username, string password)
        {

            if (String.IsNullOrEmpty(username))
            {
                return Json("UsernameIsEmpty", JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (String.IsNullOrEmpty(password))
                {
                    return Json("PasswordIsEmpty", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var customer = db.Customers.Where(x => x.Username == username && x.Password == password).FirstOrDefault();
                    if (customer != null)
                    {
                        Session["UserLogin"] = customer;
                        return Json(customer.Username, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("IncorrectUser", JsonRequestBehavior.AllowGet);
                    }
                }
            }
        }

        public ActionResult ProfileName()
        {
            if (Session["UserLogin"] != null)
            {
                ViewBag.Profile = ((Customer)Session["UserLogin"]).Username;
                return PartialView();
            }
            ViewBag.Profile = "Đăng nhập/ Đăng ký";
            return PartialView();
        }

        public ActionResult LogOut()
        {
            Session["UserLogin"] = null;
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public JsonResult Register(Customer customer)
        {
            var customerNew = new Customer();
            customerNew.FullName = customer.FullName;
            customerNew.Gender = customer.Gender;
            customerNew.Username = customer.Username;
            customerNew.Password = customer.Password;
            customerNew.Address = customer.Address;
            customerNew.Phone = customer.Phone;
            customerNew.Email = customer.Email;
            customerNew.Status = true;
            db.Customers.InsertOnSubmit(customerNew);
            db.SubmitChanges();
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ShowInfo(long id)
        {
            var customer = db.Customers.Where(x => x.Id == id).FirstOrDefault();
            if (customer != null)
            {
                return Json(new
                {
                    FullName = customer.FullName,
                    Gender = customer.Gender,
                    Username = customer.Username,
                    Address = customer.Address,
                    Phone = customer.Phone,
                    Email = customer.Email
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("CannotFindCustomer", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult UpdateInfo(long id, string fullName, bool gender, string username, string address, string phone, string email)
        {

            var customer = db.Customers.Where(x => x.Id == id).FirstOrDefault();
            customer.FullName = fullName;
            customer.Gender = gender;
            customer.Username = username;
            customer.Address = address;
            customer.Phone = phone;
            customer.Email = email;

            UpdateModel(customer);
            db.SubmitChanges();

            Session["UserLogin"] = customer;

            return Json("Success", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ChangePwd(long id, string newPwd)
        {
            var customer = db.Customers.Where(x => x.Id == id).FirstOrDefault();
            if (customer != null)
            {
                customer.Password = newPwd;
                UpdateModel(customer);
                db.SubmitChanges();
                Session["UserLogin"] = customer;
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("CannotFindCustomer", JsonRequestBehavior.AllowGet);
            }
        }
    }
}
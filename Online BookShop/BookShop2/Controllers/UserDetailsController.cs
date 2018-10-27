using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookShop2.Models;

namespace BookShop2.Controllers
{
    public class UserDetailsController : Controller
    {
        private BookShopEntities db = new BookShopEntities();

        // GET: UserDetails/Login
        public ActionResult Login()
        {

            if (Session["UserName"] == null)
                return View();
            else
            {
                if (Session["Message"] == null)
                {
                    Session["Message"] = "You alredy Login";
                }
                return RedirectToAction("Index","Home");

            }
        }
        // POSt: UserDetails/Verify
        [HttpPost]
        public ActionResult Verify(string uname,string pass)
        {
            try
            {
                if (uname == null | pass == null)
                {
                    Session["message"] = "Please Enter UserName and Password ";
                    return RedirectToAction("Login", "UserDetails");
                }
                UserDetail abc;
                abc = db.UserDetails.Find(uname);

                if (abc.Password == pass)
                {
                    Session.Clear();
                    Session["UserName"] = uname;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    Session["Message"] = "Enter Right Password";
                    return RedirectToAction("Login", "UserDetails");
                }
            }
            catch
            {
                Session["message"] = "Please Enter UserName and Password ";
                return RedirectToAction("Login", "UserDetails");
            }
        }


        // GET: UserDetails/Create

        public ActionResult Create()
        {
                if (Session["UserName"] == null)
                    return View();
                else
                {
                    return RedirectToAction("Index", "Home");

                }
         
        }

// POST: UserDetails/Create
// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserInfo([Bind(Include = "UserName,Name,Password")] UserDetail userDetail)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    db.UserDetails.Add(userDetail);
                    db.SaveChanges();

                    Session.Clear();
                    Session["UserName"] = userDetail.UserName;

                    return RedirectToAction("Login", "UserDetails");//homw par thi change karelo 6 login ma
                }

                return View(userDetail);
            }
            catch
            {
                Session["message"] = "Please Enter The Details";
                return RedirectToAction("Create", "UserDetails");
            }

        }
public ActionResult Logout()
        {
             Session.Clear();
             Session.Abandon();
             return RedirectToAction("Login", "UserDetails");
            
        }
        


    }
}

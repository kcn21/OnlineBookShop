using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BookShop2.Models;

namespace BookShop2.Controllers
{
    public class CartDetailsController : Controller
    {
        private BookShopEntities db = new BookShopEntities();

        // GET: CartDetails
        public ActionResult Index()
        {
            if (Session["UserName"] != null)
            {
                string a = Session["UserName"].ToString();
                var cartDetails = db.CartDetails.Where(c => c.UserName == a && c.Buy == 0).ToList();
                return View(cartDetails);
            }
            else
            {
                Session["Message"] = "Please Login";
                return RedirectToAction("Login", "UserDetails");
            }
        }

        // GET: CartDetails/Details/501
        public ActionResult Details(int? id)
        {
            if (Session["UserName"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CartDetail cartDetail = db.CartDetails.Find(id);
                if (cartDetail == null)
                {
                    return HttpNotFound();
                }
                return View(cartDetail);
            }
            else
            {
                Session["Message"] = "Please Login";
                return RedirectToAction("Login", "UserDetails");
            }
        }


        // GET: CartDetails/Create
        //public ActionResult Create()
        //{
        //    ViewBag.BookId = new SelectList(db.BookDetails, "BookId", "BookName");
        //    ViewBag.UserName = new SelectList(db.UserDetails, "UserName", "Name");
        //    return View();
        //}

        // POST: CartDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "CartId,UserName,BookId,Buy")] CartDetail cartDetail)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.CartDetails.Add(cartDetail);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.BookId = new SelectList(db.BookDetails, "BookId", "BookName", cartDetail.BookId);
        //    ViewBag.UserName = new SelectList(db.UserDetails, "UserName", "Name", cartDetail.UserName);
        //    return View(cartDetail);
        //}

        // GET: CartDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["UserName"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CartDetail cartDetail = db.CartDetails.Find(id);
                if (cartDetail == null)
                {
                    return HttpNotFound();
                }
                ViewBag.BookId = new SelectList(db.BookDetails, "BookId", "BookName", cartDetail.BookId);
                ViewBag.UserName = new SelectList(db.UserDetails, "UserName", "Name", cartDetail.UserName);
                return View(cartDetail);
            }
            else
            {
                Session["Message"] = "Please Login";
                return RedirectToAction("Login", "UserDetails");
            }
        }

        // POST: CartDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CartDetail cartDetail)
        {
            if (ModelState.IsValid)
            {
                CartDetail a;
                a = db.CartDetails.Find(cartDetail.CartId);
                a.Quantity = cartDetail.Quantity;
                db.Entry(a).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.BookId = new SelectList(db.BookDetails, "BookId", "BookName", cartDetail.BookId);
            //ViewBag.UserName = new SelectList(db.UserDetails, "UserName", "Name", cartDetail.UserName);
            return View(cartDetail);
        }

        // GET: CartDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["UserName"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CartDetail cartDetail = db.CartDetails.Find(id);
                if (cartDetail == null)
                {
                    return HttpNotFound();
                }
                return View(cartDetail);
            }
            else
            {
                Session["Message"] = "Please Login";
                return RedirectToAction("Login", "UserDetails");
            }
        }

        // POST: CartDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CartDetail cartDetail = db.CartDetails.Find(id);
            db.CartDetails.Remove(cartDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Buy()
        {
            if (Session["UserName"] != null)
            {
                string a = Session["UserName"].ToString();
                var cartDetails = db.CartDetails.Where(c => c.UserName == a && c.Buy == 0).ToList();
                if (cartDetails == null)
                {
                    Session["Message"] = "You Have To Add Atlest One Item";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var item in cartDetails)
                    {
                        item.Buy = 1;
                        db.Entry(item).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    return View(cartDetails);
                }
            }
            else
            {
                Session["Message"] = "Please Login";
                return RedirectToAction("Login", "UserDetails");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //[HttpPost]
        //public ActionResult Update([Bind(Include = "CartId,UserName,BookId,Buy,Quantity")] CartDetail abc)
        //{
        //    db.Entry(abc).State = EntityState.Modified;
        //    //db.CartDetails.
        //    db.SaveChanges();
        //    int a = abc.Quantity;
        //    return RedirectToAction("Index", "CartDetails");
        //}
    }
}

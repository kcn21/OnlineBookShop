using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookShop2.Models;
using PagedList;
using PagedList.Mvc;
namespace BookShop2.Controllers
{
    public class HomeController : Controller
    {
        private BookShopEntities db = new BookShopEntities();


        // GET: BookDetails
        [HandleError]
        public ActionResult Index(string searchString,string currentFilter, string author, int? price,string sortOrder,int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            ViewBag.BookNameSort = sortOrder == "BookName" ? "BookName_desc" : "BookName";
            ViewBag.AuthorSort = sortOrder == "Author" ? "Author_desc" : "Author";
            ViewBag.PriceSort = sortOrder == "Price" ? "Price_desc" : "Price";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentSort=sortOrder;
            ViewBag.CurrentFilter = searchString;
            var book = from c in db.BookDetails select c;

            if (!string.IsNullOrEmpty(searchString))
            {
                book = book.Where(c => c.BookName.Contains(searchString));
            }
            if (price != null)
            {
                book = book.Where(c => c.Price <= (int)price);
            }
            if (!string.IsNullOrEmpty(author))
            {
                book = book.Where(c => c.Author.Contains(author));
            }
          
             
            switch (sortOrder)
            {
                case "BookName_desc":
                    book = book.OrderByDescending(c =>c.BookName);
                    break;

                case "Author_desc":
                    book = book.OrderByDescending(c => c.Author);
                    break;

                case "Price_desc":
                    book = book.OrderByDescending(c => c.Price);
                    break;

                case "BookName":
                    book = book.OrderBy(c => c.BookName);
                    break;

                case "Author":
                    book = book.OrderBy(c => c.Author);
                    break;


                case "Price":
                    book = book.OrderBy(c => c.Price);
                    break;

                default:
                    book = book.OrderBy(c => c.BookName);
                    break;

                    
            } 

            return View(book.ToPagedList(pageNumber,pageSize));

        }

        // GET: BookDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookDetail bookDetail = db.BookDetails.Find(id);
            if (bookDetail == null)
            {
                return HttpNotFound();
            }
            return View(bookDetail);
        }

        //AddTOCart(Save in CartDetail)

        public ActionResult Create(int ID)
        {
            if (Session["UserName"] != null)
            {
                CartDetail cartDetail = new CartDetail();
                if (ModelState.IsValid)
                {
                    int a = (int)ID;


                    cartDetail.BookId = a;
                    cartDetail.UserName = Session["UserName"].ToString();
                    cartDetail.Buy = 0;
                    cartDetail.Quantity = 1;
                    db.CartDetails.Add(cartDetail);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    Session["Message"] = "Please Login";
                    return RedirectToAction("Login", "UserDetails");
                }
            }
            else
            {
                Session["Message"] = "Please Login";
                return RedirectToAction("Login", "UserDetails");
            }

        }
    }           
}
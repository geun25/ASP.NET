using LibraryApplication.Context;
using LibraryApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace LibraryApplication.Controllers
{
    public class HomeController : Controller
    {
        private LibraryDb db = new LibraryDb();

        public ActionResult Index()
        {
            int maxListCount = 3; // 한 페이지 당 나올수 있는 글
            int pageNum = 1;
            string keyword = Request.QueryString["keyword"] ?? string.Empty;
            string searchKind = Request.QueryString["searchKind"] ?? string.Empty;
            int totalCount = 0;

            if (Request.QueryString["page"] != null)
                pageNum = Convert.ToInt32(Request.QueryString["page"]);

            var books = new List<Book>();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                books = db.Books.ToList();

                totalCount = books.Count();
            }
            else
            {
                switch(searchKind)
                {
                    case "Title":
                        books = db.Books.Where(x => x.Title.Contains(keyword)).ToList();
                        totalCount = books.Count();
                        break;

                    case "Writer":
                        books = db.Books.Where(x => x.Writer.Contains(keyword)).ToList();
                        totalCount = books.Count();
                        break;

                    case "Publisher":
                        books = db.Books.Where(x => x.Publisher.Contains(keyword)).ToList();
                        totalCount = books.Count();
                        break;
                }                                       
            }

            books = books.OrderBy(x => x.Book_U)
                        .Skip((pageNum - 1) * maxListCount)
                        .Take(maxListCount).ToList();

            ViewBag.Page = pageNum;
            ViewBag.TotalCount = totalCount;
            ViewBag.MaxListCount = maxListCount;
            ViewBag.SearchKind = searchKind;
            ViewBag.Keyword = keyword;

            return View(books);
        }

        public ActionResult Details(int? id) 
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            Book book = db.Books.Find(id);
            
            if (book == null)
                return HttpNotFound();
            
            return View(book);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Book_U,Title,Writer,Summary,Publisher,Published_date")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges(); // 데이터베이스 저장

                return RedirectToAction("Index");
            }

            return View(book);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            Book book = db.Books.Find(id);

            if (book == null)
                return HttpNotFound();
            
            return View(book);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Book_U,Title,Writer,Summary,Publisher,Published_date")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(book);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            Book book = db.Books.Find(id);

            if (book == null)
                return HttpNotFound();
            
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);

            db.Books.Remove(book);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            
            base.Dispose(disposing);
        }
    }
}

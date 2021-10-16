using App.Models;
using System.Web.Mvc;

namespace App.Controllers
{
    public class HometwoController : Controller
    {
        public ActionResult Indextwo()
        {
            //ViewBag.Arr = new string[]
            //{
            //    "alpha",
            //    "beta",
            //    "gamma"
            //};

            ViewBag.Book = new Book
            {
                Title = "칼의 노래",
                Writer = "김훈"
            };

            return View();
        }
    }
}
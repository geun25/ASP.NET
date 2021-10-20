using ModelBindDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ModelBindDemo.Controllers
{
    public class HomeController : Controller
    {
        private Person[] personData =
        {
            new Person {PersonId = 1, Name = "김길동", Role = Role.Admin},
            new Person {PersonId = 2, Name = "홍길동", Role = Role.User},
            new Person {PersonId = 3, Name = "안길동", Role = Role.User},
            new Person {PersonId = 4, Name = "오길동", Role = Role.Guest},
        };

        public ActionResult Index(int id = 1) 
        {
            Person person = personData.Where(p => p.PersonId == id).First();
            return View(person);
        }

        public ActionResult CreatePerson()
        {
            return View(new Person());
        }

        [HttpPost]
        public ActionResult CreatePerson(Person model)
        {
            return View("Index", model);
        }
    }
}
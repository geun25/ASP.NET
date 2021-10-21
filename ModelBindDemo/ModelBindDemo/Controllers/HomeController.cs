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

        public ActionResult AddrSimple([Bind(Prefix="Addr")]AddressSimple addrSimple)
        {
            return View(addrSimple);
        }

        // 배열 바인딩
        /*
        public ActionResult Names(string[] names)
        {
            names = names ?? new string[0];
            return View(names);
        }
        */

        // 컬렉션 바인딩
        public ActionResult Names(IList<string> names)
        {
            names = names ?? new List<string>();
            return View(names); 
        }

        // 사용자 정의 모델 형식의 컬렉션 바인딩
        public ActionResult Address(IList<AddressSimple> addr)
        {
            addr = addr ?? new List<AddressSimple>();
            return View(addr);
        }
    }
}
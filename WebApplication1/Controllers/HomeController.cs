using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        static IEnumerable<Person> _persons;

        public static IEnumerable<Person> Persons => _persons ?? new List<Person>
        {
            new Person{Name = "ala",Age = 15},
            new Person{Name = "mla",Age = 12}
        };

        public ActionResult Index()
        {
            Person person = new Person{Name = "Ziutek",Age = 25};
            return View(person);
        }

        public ViewResult SecondPage()
        {
            IEnumerable<Person> persons = new List<Person>
            {
               new Person{Name = "Ziutek",Age = 25},
               new Person{Name = "Stefan",Age = 23}
        };
            return View(persons);
        }

        public ViewResult ind()
        {
            return View(Persons);
        }

        public ActionResult add()
        {
            Persons.ToList().Add(new Person { Age = 14, Name = "Arek" });
            return RedirectToAction("ind");
        }
    }
}
using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication1.Models;

/*
Tutaj jest nasz HomeController klasa c# dziedzicząca po controller odpowiedzialna za przetwarzanie żądań z i do przeglądarki
Metody controllerów sa akcjami, stąd zwracany typ ActionResult, sa jeszcze inne typy np ViewResult, JsonResult itd. ale one wszystkie dziedziczą po ActionResult


*/

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        /* Tutaj jest akcja Index Zwracająca widok Views/Home/Index z modelem czyli obiektem Typu Person*/
        public ActionResult Index()
        {
            //Tworzymy obiekt typu Person
            Person person = new Person{Name = "Ziutek",Age = 25};
            return View(person);//Przekazujemy obiekt jako argument metody View, do widoku index zostanie przekazany dokladnie ten obiekt jako model.
        }

        //Tutaj jest metoda która zwróci nam drugą strone która zawiera strone z kilkoma obiektami typu person przechowanych w prostej kolekcji
        public ViewResult SecondPage()
        {
            //IEnumerable oznacza prosta sekwencje, czyli kolekce ktora pozwoli nam enumerować po obiektach wewnatrz tej kolekcji
            // w notacji diamentowej '<>' podajemy typ obiektow wewnatrz sekwencji
            IEnumerable<Person> people = new List<Person>
            {
                //inicjalizujemy nasza sekwencje jako nowa liste z dwoma obiektmi typu person
               new Person{Name = "Ziutek",Age = 25},
               new Person{Name = "Stefan",Age = 23}
        };
            return View(people);
        }
    }
}
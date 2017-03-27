/*
  W odróżnieniu do naszego głownego controllera -HomeController, ProductController odpowiada za przetwarzanie rządań z kontekstu produktów, wiec dobra maniera jest
  utworzenie własnie nowego kontrolera.
 */

using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication1.Entities;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        //Prywatne pole, Lista naszych produktów, prwywatne pola powinny być porzedzone podłogą (dobra praktyka)
        List<Product> _products;

        //Akcja Index, zwraca vidok Views/Product/Index i jako model przekazujemy
        public ActionResult Index()
        {
            //Pobieramy sobie nasze produkty z sesji aplikacji dzięki pomocniczej metorze GetProducts()
            var products = GetProducts();
            //przekazujemy pobraną liste productów do naszego widoku jako model vidoku (Views/Product/Index)
            return View(products);
        }

        //pomocniczna metoda zwracająca domyślną liste. 
        List<Product> CreateListOfProduct()
        {
            //zwarcamy liste z odrazy przypisanymi produktami wewnatrz, nie musimy najpier deklarować listy potem dodac do niej produkty a potem ja zwrócić
            return new List<Product>
            {
                new Product{Id=1,Name = "Mleko",Category = "Do picia",Description = "Białe",Price = 10},
                new Product{Id=2,Name = "Jajko",Category = "Do jedzenia",Description = "Żółte",Price = 5},
            };
        }

        //Metoda pomocznicza zwrcająca liste produktów zapisanych w naszej sessji przeglądarki 
        List<Product> GetProducts()
        {
            // Pod nasza liste poroduktów przypisujemy z PROPERCJI Session o kluczu 'Products' i dokonujemy rzutowania/ konwersji (as) typu na Liste Productów
            _products = Session["Products"] as List<Product>;
            //Jeśli lista produktów nie jest pusta (różna od null) zwracamy te liste produktów. tu wychodzimy z metody jesli nie jest pusta lista.
            if (_products != null) return _products;

            //jesli lista jest pusta korzystamy z pomocniczej metody do utworzenia domyślnej listy produktów  i przypisanie do naszej listy.
            _products = CreateListOfProduct();
            //Musimy jeszcze do naszej sesji przypisac te liste produktów ponieważ nasza sesja jest pusta musimy do niej dopisac przykladowa liste
            //od tego mementu bedziemy konczyc metode na if'ie wyzej dopóki nie zamkniemy przegladarki i sesja wygaśnie.
            Session["Products"] = _products;

            //zwracamy liste produktów domyślnych 
            return _products;
        }
    }
}


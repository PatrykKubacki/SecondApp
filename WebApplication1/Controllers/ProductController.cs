/*
  W odróżnieniu do naszego głownego controllera -HomeController, ProductController odpowiada za przetwarzanie rządań z kontekstu produktów, wiec dobra maniera jest
  utworzenie własnie nowego kontrolera.
 */

using System.Collections.Generic;
using System.Linq;
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

        //Akcja obsługująca przycisk usuwania produktów, jako argument dostaje identyfikator
        public ActionResult Delete(int? id)
        {
            //pobieramy produkt z naszej listy o identyfikatorze równym identyfikatorowi z argumentu metody (id)
            var product = GetProducts().FirstOrDefault(p => p.Id == id);
            //Usuniecie produktu z naszej listy produktu 
            GetProducts().Remove(product);
            //Przekierowanie do metody index z tego kontrolera (jest wyżej)
            return RedirectToAction("Index");
        }

        //Metoda Edit zwracajaca formularz z produktem jest to metoda [httpget] domyslnie każda metoda jest typu get, metode post trzeba poprzedzic atrybutem [httpost]
        public ViewResult Edit(int? id)
        {
            //pobieramy produkt o takim identyfikatorze jak arugemnt metody
            var product = GetProducts().FirstOrDefault(p => p.Id == id);
            //przekazujemy go do widoku Edit (formularz edycji)
            return View(product);
        }

        //Przeciążona metoda Edit ta wyżej typu get zwraca formularz, a ta niżej typu post odbiera dane i dokonuje podmiany produktu
        [HttpPost]
        public ActionResult Edit(Product newProduct)
        {
            //pobieramy produkt o takim identyfikatorze jak ten edytowany produkt
            var product = GetProducts().FirstOrDefault(p => p.Id == newProduct.Id);
            //jesli nie ma takiego produktu dodajemy go do list
            if (product == null)
            {
                //dodanie nowego produktu do listy
                GetProducts().Add(newProduct);
                //przekierowanie do metody index czyli wyswietlenie listy produktow wszystkich
                return RedirectToAction("Index");
            }
            //ponizej podmiana danych na te nowe
            product.Category = newProduct.Category;
            product.Price = newProduct.Price;
            product.Description = newProduct.Description;
            product.Name = newProduct.Name;
            //przekierowanei do metody index 
            return RedirectToAction("Index");
        }

        //Metoda tworzy nowy produkt i przekazuje do widoku Edit, poniewaz dodawanie i edycja to taki sam formularz
        public ActionResult Create()
        {
            //pobieramy liczbe produktow w liscie
            var count = GetProducts().Count;
            //tworzenie nowego produktu 
            var product = new Product {Id = ++count} ;
            //przekazanie nowego produktu do widoku Edit, w metodzie edit nie bedzie produktu z takim identyifikatorem wiec zostanie dodawny nowy.
            return View("Edit", product);
        }
    }
}
/*
 jako identyfikator zlym podejsciem jest pobieranie (liczby elementow + 1) lepszym podejsciem jest pobieranie identyfikatora ostatniego elementu,
 W najlepszym wypadku za przyznawanie identyfikatorów odpowiada system zarządzania baza danych np MSSQLServer, Tak samo złym podejściem jest 
 żeby metody typu Edit wewnatrz ich ciała doknowywałą sie edycja, najlpeszym rozwizaniem jest uzycie wzorca Repozytorium .
     */

namespace WebApplication1.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
    }
}

/*
To jest klasa naszej Encji produkt, jak już mówiłem na zajęciach encja jest abstrakcyjnym bytem przedstawiającym nasz rzeczywisty obiekt w bazie
Pomimo, że nie korzystamy z bazy danych tylko kolekcji takie podejście bazodanowe do modelowania danych jest dobrą praktyką

    W naszej publicznej klasie znajdują sie propercje po polsku właściwośći które przechowują określone dane.

    Klasa posiada tylko i wyłącznie propercje, na taką klase mowi sie 'property-bag' z reguły unika sie budowania takich klas w których sa wyłacznie
    propercje/pola.

    Na nasze potrzeby wystarczy, klasa przechowuje id jako int, Name,Description, Category jako string (Dobrym podejsciem było by mieć osobną encje na kategorie),
    ale na nasze potrzeby categoria moze być łancuchem znaków i znajdować sie w tej własnie klasie, przechowujemy również cene jako typ decimal poniewaz ma lepsza
    dokładność niz double przez co jest chetnie wykorzystywany jako typ do przechowywania danych pieniężnych.
*/
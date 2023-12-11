using System;
using System.Collections.Generic;

// Klasa z danymi o produkcie
class Produkt
{
    public string Producent { get; set; } 
    public string Nazwa { get; set; } 
    public string Kategoria { get; set; } 
    public string KodProduktu { get; set; } 
    public double Cena { get; set; } 
    public string Opis { get; set; } 

    // Metoda dodająca produkt do magazynu
    public void DodajDoMagazynu(Magazyn magazyn)
    {
        magazyn.Produkty.Add(this);
    }

  
}

// Klasa reprezentująca magazyn
class Magazyn
{
    public List<Produkt> Produkty { get; set; } // Lista produktów w magazynie
    public Adres AdresMagazynu { get; set; }

    // Konstruktor  nowego magazyn
    public Magazyn()
    {
        Produkty = new List<Produkt>();
        AdresMagazynu = new Adres();
    }

    // Metoda dodająca produkt do listy w magazynie
    public void DodajProdukt(Produkt produkt)
    {
        Produkty.Add(produkt);
    }
}

// Klasa z danymi o magazynie
class Adres
{
    public string Ulica { get; set; } 
    public int KodPocztowy { get; set; } 
    public string Miasto { get; set; } 
    public int NumerPosesji { get; set; } 
    public int NumerLokalu { get; set; } 
}

// Główna klasa zarządzająca aplikacją
class Zarzadzanie
{
    private List<Magazyn> magazyny = new List<Magazyn>(); // Lista magazynów

    // Metoda wyświetlająca główne menu aplikacji
    public void WyswietlMenu()
    {
        Console.WriteLine("Witaj w programie zarządzającym magazynem!");
        Console.WriteLine("1. Dodaj produkt do magazynu");
        Console.WriteLine("2. Dodaj nowy magazyn");
        Console.WriteLine("3. Wyświetl informacje o magazynach");
        Console.WriteLine("4. Wyjdź z programu");

        bool dzialanie = true;

        while (dzialanie)
        {
            Console.Write("Wybierz opcję: ");
            string wybor = Console.ReadLine();

            switch (wybor)
            {
                case "1":
                    DodajProdukt();
                    break;
                case "2":
                    DodajMagazyn();
                    break;
                case "3":
                    WyswietlInformacjeOMagazynach();
                    break;
                case "4":
                    Console.WriteLine("Zamykanie Aplikacji");
                    dzialanie = false;
                    break;
                default:
                    Console.WriteLine("Niepoprawny wybór");
                    break;
            }
        }
    }

    // Metoda dodająca nowy produkt do magazynu
    private void DodajProdukt()
    {
        if (magazyny.Count == 0)
        {
            Console.WriteLine("Nie ma jeszcze żadnego magazynu. Najpierw dodaj magazyn.");
            return;
        }
        Produkt nowyProdukt = new Produkt();

        Console.Write("Nazwa producenta: ");
        nowyProdukt.Producent = Console.ReadLine();

        Console.Write("Nazwa produktu: ");
        nowyProdukt.Nazwa = Console.ReadLine();

        Console.Write("Kategoria: ");
        nowyProdukt.Kategoria = Console.ReadLine();

        Console.Write("Kod produktu (tylko cyfry): ");
        string kodProduktuInput = Console.ReadLine();

        if (CzyJestLiczba(kodProduktuInput))
        {
            nowyProdukt.KodProduktu = kodProduktuInput;
        }
        else
        {
            Console.WriteLine("Kod produktu powinien składać się tylko z cyfr");
            DodajProdukt();
            return;
        }

        Console.Write("Cena: ");
        double cena;
        if (double.TryParse(Console.ReadLine(), out cena))
        {
            nowyProdukt.Cena = cena;
        }
        else
        {
            Console.WriteLine("Niepoprawna cena");
            DodajProdukt();
            return;
        }

        Console.Write("Opis: ");
        nowyProdukt.Opis = Console.ReadLine();

        magazyny[0].DodajProdukt(nowyProdukt);
        Console.WriteLine("Nowy produkt dodany do magazynu");
    }

    // Metoda dodająca nowy magazyn
    private void DodajMagazyn()
    {
        Magazyn nowyMagazyn = new Magazyn();
        Adres nowyAdres = new Adres();

        Console.Write("Ulica: ");
        nowyAdres.Ulica = Console.ReadLine();

        Console.Write("Kod pocztowy: ");
        int kodPocztowy;
        if (int.TryParse(Console.ReadLine(), out kodPocztowy))
        {
            nowyAdres.KodPocztowy = kodPocztowy;
        }
        else
        {
            Console.WriteLine("Niepoprawny kod pocztowy");
            DodajMagazyn();
            return;
        }

        Console.Write("Miasto: ");
        nowyAdres.Miasto = Console.ReadLine();

        Console.Write("Numer posesji: ");
        int numerPosesji;
        if (int.TryParse(Console.ReadLine(), out numerPosesji))
        {
            nowyAdres.NumerPosesji = numerPosesji;
        }
        else
        {
            Console.WriteLine("Niepoprawny numer posesji");
            DodajMagazyn();
            return;
        }

        Console.Write("Numer lokalu: ");
        int numerLokalu;
        if (int.TryParse(Console.ReadLine(), out numerLokalu))
        {
            nowyAdres.NumerLokalu = numerLokalu;
        }
        else
        {
            Console.WriteLine("Niepoprawny numer lokalu");
            DodajMagazyn();
            return;
        }

        nowyMagazyn.AdresMagazynu = nowyAdres;
        magazyny.Add(nowyMagazyn);
        Console.WriteLine("Nowy magazyn dodany");
    }

    // Metoda wyświetlająca informacje o magazynach
    private void WyswietlInformacjeOMagazynach()
    {
        Console.WriteLine("Informacje o magazynach:");

        foreach (var magazyn in magazyny)
        {
            WypiszInformacjeOMagazynie(magazyn);
            WypiszProduktyWMagazynie(magazyn);
            Console.WriteLine();
        }

        bool wybor = false;
        while (!wybor)
        {
            Console.WriteLine("Naciśnij 'd', aby powrócić do menu głównego.");

            string userInput = Console.ReadLine();
            if (userInput.ToLower() == "d")
            {
                WyswietlMenu();
                wybor = true;
            }
            else
            {
                Console.WriteLine("Niepoprawna opcja");
            }
        }
    }

    // Metoda wyświetlająca informacje o adresie magazynu
    private void WypiszInformacjeOMagazynie(Magazyn magazyn)
    {
        Console.WriteLine($"Adres: {magazyn.AdresMagazynu.Ulica}, {magazyn.AdresMagazynu.KodPocztowy}, {magazyn.AdresMagazynu.Miasto}");
    }

    // Metoda wyświetlająca produkty w magazynie
    private void WypiszProduktyWMagazynie(Magazyn magazyn)
    {
        Console.WriteLine("Produkty w magazynie:");
        foreach (var produkt in magazyn.Produkty)
        {
            Console.WriteLine($"- {produkt.Nazwa}, Cena: {produkt.Cena}");
        }
    }

    // Metoda sprawdzająca, czy wprowadzona wartość jest liczbą
    private bool CzyJestLiczba(string input)
    {
        return int.TryParse(input, out _);
    }
}

// Klasa główna uruchamiająca aplikację

class Program
{
    static void Main(string[] args)
    {
        Zarzadzanie aplikacja = new Zarzadzanie();
        aplikacja.WyswietlMenu();
    }
}

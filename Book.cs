namespace CA241202;

internal class Book
{
    public long ISBN { get; private set; }
    public List<Author> Szerzok { get; private set; }
    public string Cim { get; private set; }
    public int Ev { get; private set; }
    public string Nyelv { get; private set; }
    public int Keszlet { get; set; }
    public int Ar { get; private set; }

    public Book(long isbn, string cim, int ev, string nyelv, int ar, int keszlet, string[] szerzok)
    {
        if (isbn.ToString().Length != 10)
            throw new ArgumentException("Az ISBN pontosan 10 karakter lehet");
        if (cim.Length < 3 || cim.Length > 64)
            throw new ArgumentException("A Cím hossza 3 és 64 között lehet");
        if (ev < 2007 || ev > DateTime.Now.Year)
            throw new ArgumentException("Az év 2007 és a jelenlegi év között lehet");
        if (!new[] { "angol", "német", "magyar" }.Contains(nyelv.ToLower()))
            throw new ArgumentException("A Nyelv angol, német vagy magyar lehet");
        if (ar < 1000 || ar > 10000 || ar % 100 != 0)
            throw new ArgumentException("Az ár 1000 és 10000 között lehet és csak 100 többszöröse");
        if (szerzok.Length < 1 || szerzok.Length > 3)
            throw new ArgumentException("A szerzők listában csak 1 és 3 közötti szerzők darabszáma lehet");
        if (keszlet < 0 || keszlet > 10)
            throw new ArgumentException("A készlet 0 és 10 között lehet");

        ISBN = isbn;
        Cim = cim;
        Ev = ev;
        Nyelv = DetermineLanguage(cim);
        Ar = ar;
        Keszlet = keszlet;
        Szerzok = szerzok.Select(name => new Author(name)).ToList();
    }

    public Book(string cim, string szerzok)
    {
        ISBN = new Random().NextInt64(1000000000, 9999999999);
        Cim = cim;
        Ev = 2024;
        Nyelv = DetermineLanguage(cim);
        Ar = 4500;
        Keszlet = 2;
        Szerzok = new List<Author> { new Author(szerzok) };
    }

    private string DetermineLanguage(string cim)
    {
        // Add logic to determine the language based on the title
        if (cim.Contains("English"))
        {
            return "angol";
        }
        else if (cim.Contains("Deutsch"))
        {
            return "német";
        }
        else
        {
            return "magyar";
        }
    }

    public override string ToString()
    {
        string szerzoCimke = Szerzok.Count == 1 ? "Szerző" : "Szerzők";
        string keszletAllapot = Keszlet == 0 ? "Kifogyott" : $"{Keszlet} darab";
        return $"{szerzoCimke}: {string.Join(", ", Szerzok.Select(a => $"{a.VezetekNev} {a.KeresztNev}"))}, Cím: {Cim}, Év: {Ev}, Nyelv: {Nyelv}, Price: {Ar}, Ár: {keszletAllapot}";
    }

    private static void EladasokEmulalasa(List<Book> konyvek)
    {
        if (konyvek == null || konyvek.Count == 0)
        {
            Console.WriteLine("Nincsenek elérhető könyvek eladásra.");
            return;
        }

        var rnd = new Random();
        int osszesBevetel = 0;
        int kifogyottKonyvekSzama = 0;
        int kezdetiKeszlet = konyvek.Sum(b => b.Keszlet);

        for (int i = 0; i < 100; i++)
        {
            if (konyvek.Count == 0)
            {
                Console.WriteLine("Minden könyv elfogyott.");
                break;
            }

            var konyv = konyvek[rnd.Next(konyvek.Count)];
            if (konyv.Keszlet > 0)
            {
                konyv.Keszlet--;
                osszesBevetel += konyv.Ar;
            }
            else
            {
                if (rnd.NextDouble() < 0.5)
                {
                    konyv.Keszlet = rnd.Next(1, 11);
                }
                else
                {
                    konyvek.Remove(konyv);
                    kifogyottKonyvekSzama++;
                }
            }
        }

        int vegsoKeszlet = konyvek.Sum(b => b.Keszlet);
        int keszletKulonbseg = vegsoKeszlet - kezdetiKeszlet;

        Console.WriteLine($"Összes bevétel: {osszesBevetel}");
        Console.WriteLine($"Kifogyott könyvek száma: {kifogyottKonyvekSzama}");
        Console.WriteLine($"Kezdeti készlet: {kezdetiKeszlet}, Végső készlet: {vegsoKeszlet}, Készlet különbség: {keszletKulonbseg}");
    }
}

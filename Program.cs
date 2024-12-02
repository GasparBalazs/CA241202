using CA241202;

public class Program
{
    public static void Main()
    {
        var konyvek = KonyvekGeneralasa();

        foreach (var book in konyvek)
        {
            Console.WriteLine(book.ToString());
        }

        EladasokEmulalasa(konyvek);
    }

    private static List<Book> KonyvekGeneralasa()
    {
        var konyvek = new List<Book>();
        var rnd = new Random();
        var cimek = new[] { "Echoes of Eternity", "The Silent Forest", "Das verlorene Königreich", "Flüstern im Wind", "Az örökkévalóság visszhangjai", "A csendes erdő", "The Lost Kingdom", "Whispers in the Wind", "Das Geheimnis der Insel", "Geschichten der Alten", "Az elfeledett ösvény", "A mélység titkai", "The Enchanted Garden", "Shadows of the Past", "Der verzauberte Garten", "Schatten der Vergangenheit", "A varázslatos kert", "A múlt árnyai", "The Forgotten Path", "Mysteries of the Deep", "Der vergessene Pfad", "Geheimnisse der Tiefe", "Az elfeledett ösvény", "A mélység titkai", "The Hidden Treasure", "Journey to the Stars", "Der verborgene Schatz", "Reise zu den Sternen", "A rejtett kincs", "Utazás a csillagokhoz", "The Secret of the Island", "Tales of the Ancient", "Das Geheimnis der Insel", "Geschichten der Alten", "A sziget titka", "Az ősi mesék", "The Last Adventure", "Legends of the Night", "Das letzte Abenteuer", "Legenden der Nacht", "Az utolsó kaland", "Az éjszaka legendái", "The Silent Forest", "The Eternal Quest", "Der stille Wald", "Die ewige Suche", "A csendes erdő", "Az örök keresés" };
        var szerzok = new[] { "John Doe", "Jane Smith", "Max Mustermann", "Emily Johnson", "Michael Brown", "Sarah Davis", "David Wilson", "Laura Martinez", "James Anderson", "Linda Taylor", "Robert Thomas", "Patricia Moore", "Charles Jackson", "Barbara White", "Christopher Harris", "Susan Martin", "Daniel Thompson", "Jessica Garcia", "Matthew Martinez", "Karen Robinson", "Anthony Clark", "Nancy Lewis", "Mark Rodriguez", "Betty Lee", "Paul Walker", "Sandra Hall", "Steven Allen", "Donna Young", "Andrew King", "Carol Wright" };

        while (konyvek.Count < 15)
        {
            var cim = cimek[rnd.Next(cimek.Length)];
            var nyelv = rnd.NextDouble() < 0.8 ? "magyar" : "angol";
            var ev = rnd.Next(2007, DateTime.Now.Year + 1);
            var ar = rnd.Next(10, 101) * 100;
            var keszlet = rnd.Next(0, 5);
            var szerzoSzam = rnd.NextDouble() < 0.7 ? 1 : rnd.Next(2, 4);
            var konyvSzerzok = szerzok.OrderBy(_ => rnd.Next()).Take(szerzoSzam).ToArray();
            var isbn = rnd.NextInt64(1000000000, 9999999999);

            if (konyvek.Any(b => b.ISBN == isbn)) continue;

            var book = new Book(isbn, cim, ev, nyelv, ar, keszlet, konyvSzerzok);
            konyvek.Add(book);
        }

        return konyvek;
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
                    konyv.Keszlet = rnd.Next(1, 11);
                else
                {
                    konyvek.Remove(konyv);
                    kifogyottKonyvekSzama++;
                }
            }
        }

        int vegsoKeszlet = konyvek.Sum(b => b.Keszlet);
        int keszletKulonbseg = kezdetiKeszlet - vegsoKeszlet;

        Console.WriteLine($"Összes bevétel: {osszesBevetel} Ft");
        Console.WriteLine($"Kifogyott könyvek száma: {kifogyottKonyvekSzama}");
        Console.WriteLine($"Kezdeti készlet: {kezdetiKeszlet}, Végső készlet: {vegsoKeszlet}, Készlet különbség: {keszletKulonbseg}");
    }
}

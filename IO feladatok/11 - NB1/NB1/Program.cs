using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        var sorok = File.ReadAllLines("adatok.txt", Encoding.UTF8);

        var jatekosok = sorok
            .Select(s => s.Split('\t'))
            .Select(x => new
            {
                Klub = x[0].Trim(),
                Mezszam = x[1].Trim(),
                Utonev = x[2]?.Trim() ?? "",
                Vezeteknev = x[3]?.Trim() ?? "",
                SzuletesiDatum = DateTime.ParseExact(x[4].Trim(), "yyyy.MM.dd.", CultureInfo.InvariantCulture),
                Magyar = x[5]?.Trim() ?? "0",     // most stringben marad
                Kulfoldi = x[6]?.Trim() ?? "0",   // most stringben marad
                Ertek = int.Parse(x[7]),
                Poszt = x[8]?.Trim() ?? ""
            })
            .ToList();

        Console.WriteLine($"Beolvasott játékosok: {jatekosok.Count}");

        var mezony = jatekosok.Where(j => !j.Poszt.Equals("kapus", StringComparison.CurrentCultureIgnoreCase));
        var legidosebb = mezony.OrderBy(j => j.SzuletesiDatum).First();
        Console.WriteLine($"Legidősebb mezőnyjátékos: {legidosebb.Vezeteknev} {legidosebb.Utonev}, szül.: {legidosebb.SzuletesiDatum:yyyy.MM.dd.}");

        int magyar = jatekosok.Count(j => j.Magyar == "-1" && j.Kulfoldi != "-1");
        int kulfoldi = jatekosok.Count(j => j.Kulfoldi == "-1" && j.Magyar != "-1");
        int kettos = jatekosok.Count(j => j.Magyar == "-1" && j.Kulfoldi == "-1");
        Console.WriteLine($"Magyar: {magyar}, Külföldi: {kulfoldi}, Kettős: {kettos}");

        var csapatErtek = jatekosok
            .GroupBy(j => j.Klub)
            .Select(g => new { Klub = g.Key, OsszErtek = g.Sum(x => x.Ertek) })
            .OrderByDescending(x => x.OsszErtek);
        Console.WriteLine("\nCsapatonkénti összérték (ezer €):");
        foreach (var c in csapatErtek)
            Console.WriteLine($"{c.Klub}: {c.OsszErtek}");

        var egyPoszt = jatekosok
            .GroupBy(j => j.Klub)
            .SelectMany(g => g.GroupBy(j => j.Poszt)
                              .Where(pg => pg.Count() == 1)
                              .Select(pg => new { Klub = g.Key, Poszt = pg.Key }))
            .OrderBy(x => x.Klub);
        Console.WriteLine("\nCsak egy játékossal rendelkező posztok:");
        foreach (var e in egyPoszt)
            Console.WriteLine($"{e.Klub} – {e.Poszt}");

        double atlag = jatekosok.Average(j => j.Ertek);
        var alattiak = jatekosok
            .Where(j => j.Ertek <= atlag)
            .OrderBy(j => j.Klub)
            .ThenByDescending(j => j.Ertek);
        Console.WriteLine($"\nÁtlagérték: {atlag:F2} ezer €\nÁtlag alatti játékosok:");
        foreach (var j in alattiak)
            Console.WriteLine($"{j.Vezeteknev} {j.Utonev} – {j.Klub} – {j.Poszt} – {j.Ertek}e€");

        DateTime ma = DateTime.Today;
        var fiatalok = jatekosok
            .Where(j => j.Magyar == "-1")
            .Where(j =>
            {
                int kor = ma.Year - j.SzuletesiDatum.Year;
                if (ma < j.SzuletesiDatum.AddYears(kor)) kor--;
                return kor >= 18 && kor <= 21;
            })
            .OrderBy(j => j.Klub)
            .ThenBy(j => j.SzuletesiDatum)
            .ToList();
        Console.WriteLine("\n18–21 éves magyar játékosok:");
        if (!fiatalok.Any())
            Console.WriteLine("Nincs ilyen játékos.");
        else
            foreach (var j in fiatalok)
                Console.WriteLine($"{j.Vezeteknev} {j.Utonev} – {j.SzuletesiDatum:yyyy.MM.dd.} – {j.Klub}");

        using (var hazai = new StreamWriter("hazai.txt", false, Encoding.UTF8))
        {
            foreach (var csoport in jatekosok.Where(j => j.Magyar == "-1").GroupBy(j => j.Klub).OrderBy(g => g.Key))
            {
                hazai.WriteLine(csoport.Key);
                foreach (var j in csoport.OrderBy(j => j.Vezeteknev).ThenBy(j => j.Utonev))
                    hazai.WriteLine($"  - {j.Vezeteknev} {j.Utonev} – {j.Poszt} – {j.Ertek} ezer €");
                hazai.WriteLine();
            }
        }

        using (var legios = new StreamWriter("legios.txt", false, Encoding.UTF8))
        {
            foreach (var csoport in jatekosok.Where(j => j.Kulfoldi == "-1").GroupBy(j => j.Klub).OrderBy(g => g.Key))
            {
                legios.WriteLine(csoport.Key);
                foreach (var j in csoport.OrderBy(j => j.Vezeteknev).ThenBy(j => j.Utonev))
                    legios.WriteLine($"  - {j.Vezeteknev} {j.Utonev} – {j.Poszt} – {j.Ertek} ezer €");
                legios.WriteLine();
            }
        }

        Console.WriteLine("\nKiírva: hazai.txt és legios.txt");
    }
}

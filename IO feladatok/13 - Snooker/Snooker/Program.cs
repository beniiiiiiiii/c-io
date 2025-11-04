using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        var sorok = File.ReadAllLines("snooker.txt", Encoding.UTF8)
                        .Skip(1) // fejléc átugrása
                        .Where(s => !string.IsNullOrWhiteSpace(s))
                        .ToList();

        var versenyzok = sorok
            .Select(s => s.Split(';'))
            .Select(x => new
            {
                Helyezes = int.Parse(x[0]),
                Nev = x[1].Trim(),
                Orszag = x[2].Trim(),
                Nyeremeny = int.Parse(x[3])
            })
            .ToList();

        Console.WriteLine($"A világranglistán {versenyzok.Count} versenyző szerepel");

        double atlag = versenyzok.Average(v => v.Nyeremeny);
        Console.WriteLine($"A versenyzők átlagosan {atlag:0} fontot kerestek");

        var legjobbKina = versenyzok
            .Where(v => v.Orszag.StartsWith("Kína", StringComparison.CurrentCultureIgnoreCase))
            .OrderByDescending(v => v.Nyeremeny)
            .FirstOrDefault();

        if (legjobbKina != null)
        {
            double atvaltott = legjobbKina.Nyeremeny * 380;
            Console.WriteLine($"A legjobban kereső kínai versenyző:");
            Console.WriteLine($"Helyezés: {legjobbKina.Helyezes}");
            Console.WriteLine($"Név: {legjobbKina.Nev}");
            Console.WriteLine($"Ország: {legjobbKina.Orszag}");
            Console.WriteLine($"Nyereménye fontban: {legjobbKina.Nyeremeny:N0}");
            Console.WriteLine($"Nyereménye forintban: {atvaltott:N0} Ft");
        }
        else
        {
            Console.WriteLine("Nincs kínai versenyző az adatok között!");
        }

        bool vanNorveg = versenyzok.Any(v => v.Orszag.Equals("Norvégia", StringComparison.CurrentCultureIgnoreCase));
        Console.WriteLine(vanNorveg
            ? "Van a versenyzők között norvég versenyző."
            : "Nincs a versenyzők között norvég versenyző.");

        var stat = versenyzok
            .GroupBy(v => v.Orszag)
            .Select(g => new { Orszag = g.Key, Darab = g.Count() })
            .Where(x => x.Darab > 4)
            .OrderByDescending(x => x.Darab)
            .ThenBy(x => x.Orszag);

        Console.WriteLine("\nStatisztika (legalább 5 versenyző):");
        foreach (var s in stat)
            Console.WriteLine($"{s.Orszag} - {s.Darab} fő");
    }
}

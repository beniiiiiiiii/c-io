using Snooker;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

// 2. feladat
var jatekosok = File.ReadAllLines("snooker.txt")
    .Skip(1)
    .Select(sor =>
    {
        var darabok = sor.Split(';');
        return new Jatekos
        {
            Helyezes = int.Parse(darabok[0].Trim()),
            Nev = darabok[1].Trim(),
            Orszag = darabok[2].Trim()
                 .Replace("�", "i")
                 .Replace("Kina", "Kína")
                 .Replace("Kina", "Kína"),
            Nyeremeny = int.Parse(darabok[3].Trim())
        };
    })
    .ToList();
// 3. feladat
Console.WriteLine("3. feladat: A világranglistán {0} versenyző szerepel", jatekosok.Count);

// 4. feladat
double atlag = jatekosok.Average(j => j.Nyeremeny);
Console.WriteLine("4. feladat: A versenyzők átlagosan {0:F2} fontot kerestek", atlag);

// 5. feladat
var legjobbKina = jatekosok
    .Where(j => j.Orszag.Trim().Equals("Kína", StringComparison.OrdinalIgnoreCase))
    .OrderByDescending(j => j.Nyeremeny)
    .FirstOrDefault();

Console.WriteLine("5. feladat: A legjobban kereső kínai versenyző:");
Console.WriteLine("Helyezés: {0}", legjobbKina.Helyezes);
Console.WriteLine("Név: {0}", legjobbKina.Nev);
Console.WriteLine("Ország: {0}", legjobbKina.Orszag);
Console.WriteLine("Nyeremény összege: {0:N0} Ft", legjobbKina.Nyeremeny * 380);

// 6. feladat
bool vanNorveg = jatekosok.Any(j => j.Orszag.Contains("Norvégia"));
Console.WriteLine("6. feladat: A versenyzők között {0}norvég versenyző.",
    vanNorveg ? "van " : "nincs ");

// 7. feladat
Console.WriteLine("7. feladat: Statisztika");
var stat = jatekosok
    .GroupBy(j => j.Orszag)
    .Where(g => g.Count() > 4)
    .OrderBy(g => g.Key);

foreach (var csoport in stat)
{
    Console.WriteLine("{0} - {1} fő", csoport.Key, csoport.Count());
}

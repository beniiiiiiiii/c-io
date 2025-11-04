using System;
using System.IO;
using System.Linq;
using System.Globalization;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var sorok = File.ReadAllLines("EuCsatlakozas.txt");
        var orszagok = sorok
            .Select(s => s.Split(';'))
            .Select(x => new
            {
                Orszag = x[0],
                Datum = DateTime.ParseExact(x[1], "yyyy.MM.dd", CultureInfo.InvariantCulture)
            })
            .ToList();

        Console.WriteLine("EU tagállamainak száma: {0} db", orszagok.Count);

        var ev2007 = orszagok.Count(x => x.Datum.Year == 2007);
        Console.WriteLine("2007-ben {0} ország csatlakozott.", ev2007);

        var magyar = orszagok.FirstOrDefault(x => x.Orszag.StartsWith("Magyar"));
        if (magyar != null)
            Console.WriteLine("Magyarország csatlakozásának dátuma: {0:yyyy.MM.dd}", magyar.Datum);
        else
            Console.WriteLine("Magyarország nem található az adatok között!");


        bool voltMajusi = orszagok.Any(x => x.Datum.Month == 5);
        Console.WriteLine("{0}", voltMajusi ? "Májusban volt csatlakozás!" : "Nem volt májusban csatlakozás.");

        var legkesobbi = orszagok.MaxBy(x => x.Datum);
        Console.WriteLine("Legutoljára csatlakozott ország: {0}", legkesobbi.Orszag);

        Console.WriteLine("Statisztika");
        var stat = orszagok
            .GroupBy(x => x.Datum.Year)
            .OrderBy(x => x.Key)
            .Select(g => new { Év = g.Key, Db = g.Count() });

        foreach (var e in stat)
            Console.WriteLine($"{e.Év} - {e.Db} ország");
    }
}

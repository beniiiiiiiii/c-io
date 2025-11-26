using EU_Tagallamok;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

// 2. feladat
var orszagok = File.ReadAllLines("EUcsatlakozas.txt")
    .Select(sor =>
    {
        var darabok = sor.Split(';');
        return new Country
        {
            Name = darabok[0].Trim(),
            JoinDate = DateTime.Parse(darabok[1].Trim())
        };
    })
    .ToList();

// 3. feladat
Console.WriteLine("3. feladat: EU tagállamainak száma: {0} db", orszagok.Count);

// 4. feladat
var csatlakozott2007 = orszagok.Count(o => o.JoinDate.Year == 2007);
Console.WriteLine("4. feladat: 2007-ben {0} ország csatlakozott.", csatlakozott2007);

// 5. feladat
var magyarorszag = orszagok.FirstOrDefault(o => o.Name == "Magyarország");
if (magyarorszag != null)
    Console.WriteLine("5. feladat: Magyarország csatlakozásának dátuma: {0:yyyy.MM.dd}", magyarorszag.JoinDate);

// 6. feladat
bool voltMajusi = orszagok.Any(o => o.JoinDate.Month == 5);
Console.WriteLine("6. feladat: {0}", voltMajusi ? "Májusban volt csatlakozás!" : "Májusban nem volt csatlakozás!");

// 7. feladat
var utolso = orszagok.OrderByDescending(o => o.JoinDate).First();
Console.WriteLine("7. feladat: Legutoljára csatlakozott ország: {0}", utolso.Name);

// 8. feladat
Console.WriteLine("8. feladat: Statisztika");
var stat = orszagok
    .GroupBy(o => o.JoinDate.Year)
    .OrderBy(g => g.Key);

foreach (var ev in stat)
{
    Console.WriteLine("{0} - {1} ország", ev.Key, ev.Count());
}
Console.ReadLine();
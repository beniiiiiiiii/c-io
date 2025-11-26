using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;

Console.OutputEncoding = Encoding.UTF8;

var sorok = File.ReadAllLines("vb2018.txt", Encoding.UTF8)
                .Skip(1)
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToList();

List<Stadion> stadionok = sorok
    .Select(s => s.Split(';'))
    .Select(x => new Stadion(
        x[0].Trim(),
        x[1].Trim(),
        x[2].Trim(),
        int.Parse(x[3])
    ))
    .ToList();

Console.WriteLine($"3. feladat: Stadionok száma: {stadionok.Count}");

var legkevesebb = stadionok.OrderBy(s => s.Ferohely).First();
Console.WriteLine("4. feladat: A legkevesebb férőhely:");
Console.WriteLine($"Város: {legkevesebb.Varos}");
Console.WriteLine($"Stadion neve: {legkevesebb.Nev1}");
Console.WriteLine($"Férőhely: {legkevesebb.Ferohely}");

double atlag = stadionok.Average(s => s.Ferohely);
Console.WriteLine($"5. feladat: Átlagos férőhely: {atlag:0}");

int ismertNev2 = stadionok.Count(s => s.Nev2 != "n.a.");
Console.WriteLine($"6. feladat: Két néven is ismert stadionok száma: {ismertNev2}");

string varos;
do
{
    Console.Write("7. feladat: Kérem a város nevét: ");
    varos = Console.ReadLine().Trim();
}
while (varos.Length < 3);

var keresett = stadionok.FirstOrDefault(s =>
    s.Varos.Equals(varos, StringComparison.CurrentCultureIgnoreCase));

if (keresett != null)
{
    Console.WriteLine($"A keresett város VB helyszín volt.");
}
else
{
    Console.WriteLine($"Nem volt ilyen város a VB helyszínek között.");
}

int kulonbozo = stadionok
    .Select(s => s.Varos)
    .Distinct(StringComparer.CurrentCultureIgnoreCase)
    .Count();

Console.WriteLine($"9. feladat: {kulonbozo} különböző városban voltak mérkőzések.");
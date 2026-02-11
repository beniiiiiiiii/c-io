using Hivasok;

var sorok = File.ReadAllLines("hivasok.txt");
var hivasok = new List<Hivas>();

for (int i = 0; i < sorok.Length; i += 2)
{
    var ido = sorok[i].Split().Select(int.Parse).ToArray();
    hivasok.Add(new Hivas
    {
        Kezdet = new TimeSpan(ido[0], ido[1], ido[2]),
        Vege = new TimeSpan(ido[3], ido[4], ido[5]),
        Telefonszam = sorok[i + 1]
    });
}

Console.Write("Adjon meg egy telefonszámot: ");
string szam = Console.ReadLine();

bool mobil = szam.StartsWith("39") ||
             szam.StartsWith("41") ||
             szam.StartsWith("71");

Console.WriteLine(mobil ? "Mobil szám" : "Vezetékes szám");
Console.WriteLine();

Console.Write("Kezdő idő (óra perc mp): ");
var k = Console.ReadLine().Split().Select(int.Parse).ToArray();

Console.Write("Vége idő (óra perc mp): ");
var v = Console.ReadLine().Split().Select(int.Parse).ToArray();

var kezdet = new TimeSpan(k[0], k[1], k[2]);
var vege = new TimeSpan(v[0], v[1], v[2]);

int percek = (int)Math.Ceiling((vege - kezdet).TotalMinutes);
Console.WriteLine($"Számlázott percek: {percek}");
Console.WriteLine();

File.WriteAllLines("percek.txt",
    hivasok.Select(h => $"{h.SzamlazottPercek()} {h.Telefonszam}"));

int csucs = hivasok.Count(h => h.Csucsido());
int nemCsucs = hivasok.Count - csucs;

Console.WriteLine($"Csúcsidőben indított hívások: {csucs}");
Console.WriteLine($"Csúcsidőn kívüli hívások: {nemCsucs}");
Console.WriteLine();

int mobilPercek = hivasok
    .Where(h => h.Mobil())
    .Sum(h => h.SzamlazottPercek());

int vezetekesPercek = hivasok
    .Where(h => !h.Mobil())
    .Sum(h => h.SzamlazottPercek());

Console.WriteLine($"Mobil percek: {mobilPercek}");
Console.WriteLine($"Vezetékes percek: {vezetekesPercek}");
Console.WriteLine();

double osszeg = hivasok
    .Where(h => h.Csucsido())
    .Sum(h =>
    {
        double ar = h.Mobil() ? 69.175 : 30;
        return ar * h.SzamlazottPercek();
    });

Console.WriteLine($"Csúcsdíjas hívások összege: {osszeg:F2} Ft");
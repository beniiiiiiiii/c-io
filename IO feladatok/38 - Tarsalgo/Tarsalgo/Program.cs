using Tarsalgo;

var sorok = File.ReadAllLines("ajto.txt");
var lista = new List<Athaladas>();

foreach (var sor in sorok)
{
    var adatok = sor.Split();
    lista.Add(new Athaladas
    {
        Idopont = new TimeSpan(int.Parse(adatok[0]), int.Parse(adatok[1]), 0),
        Azonosito = int.Parse(adatok[2]),
        Irany = adatok[3]
    });
}

var elsoBe = lista.First(x => x.Irany == "be").Azonosito;
var utolsoKi = lista.Last(x => x.Irany == "ki").Azonosito;

Console.WriteLine($"Az elso belepo: {elsoBe}");
Console.WriteLine($"Az utolso kilepo: {utolsoKi}");
Console.WriteLine();

File.WriteAllLines("athaladas.txt",
    lista.GroupBy(x => x.Azonosito)
         .OrderBy(g => g.Key)
         .Select(g => $"{g.Key} {g.Count()}"));

var bent = new HashSet<int>();

foreach (var e in lista)
{
    if (e.Irany == "be")
        bent.Add(e.Azonosito);
    else
        bent.Remove(e.Azonosito);
}

Console.WriteLine("A vegen bent voltak:");
foreach (var id in bent.OrderBy(x => x))
    Console.Write(id + " ");
Console.WriteLine();
Console.WriteLine();

int aktualis = 0;
int max = 0;
TimeSpan maxIdo = new TimeSpan();

foreach (var e in lista)
{
    if (e.Irany == "be")
        aktualis++;
    else
        aktualis--;

    if (aktualis > max)
    {
        max = aktualis;
        maxIdo = e.Idopont;
    }
}

Console.WriteLine($"Peldaul {maxIdo:hh\\:mm}-kor voltak a legtobben.");
Console.WriteLine();

Console.Write("Adja meg a szemely azonositojat: ");
int keresett = int.Parse(Console.ReadLine());
Console.WriteLine();

TimeSpan? belepes = null;
int osszesPerc = 0;
bool bentVan = false;

foreach (var e in lista.Where(x => x.Azonosito == keresett))
{
    if (e.Irany == "be")
    {
        belepes = e.Idopont;
        bentVan = true;
    }
    else
    {
        Console.WriteLine($"{belepes:hh\\:mm}-{e.Idopont:hh\\:mm}");
        osszesPerc += (int)(e.Idopont - belepes.Value).TotalMinutes;
        bentVan = false;
    }
}

if (bentVan)
{
    TimeSpan vege = new TimeSpan(15, 0, 0);
    Console.WriteLine($"{belepes:hh\\:mm}-");
    osszesPerc += (int)(vege - belepes.Value).TotalMinutes;
}

Console.WriteLine();
Console.WriteLine($"A(z) {keresett}. szemely osszesen {osszesPerc} percet volt bent, " +
    (bentVan ? "a megfigyeles vegen bent volt." : "a megfigyeles vegen nem volt bent."));
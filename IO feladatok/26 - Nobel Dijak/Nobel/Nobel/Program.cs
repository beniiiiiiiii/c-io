using Nobel;

var lines = File.ReadAllLines("nobel.csv").Skip(1);
var awards = new List<Award>();

foreach (var line in lines)
{
    var columns = line.Split(';');
    var award = new Award
    {
        Year = int.Parse(columns[0]),
        Type = columns[1],
        FirstName = columns[2],
        LastName = columns[3]
    };
    awards.Add(award);
}
foreach (var award in awards)
{
    Console.WriteLine(award);
}

//feladat 3
var arthur = awards.FirstOrDefault(a => a.FirstName == "Arthur B.");
Console.WriteLine($"\nArthur B. McDonald {arthur.Type} díjat kaptt");

//feladat 4
var literatureAward2017 = awards.FirstOrDefault(a => a.Year == 2017 && a.Type == "irodalmi");
Console.WriteLine($"\n2017-ben irodalmi díjat kapott: {literatureAward2017.FirstName} {literatureAward2017.LastName}");

//feladat 5
var orgsPeaceAwardsFrom1990ToNow = awards.Where(a => string.IsNullOrEmpty(a.LastName) && a.Year >= 1990);
Console.WriteLine("\n1990 óta a következő szervezetek kaptak békedíjat:");
foreach (var award in orgsPeaceAwardsFrom1990ToNow)
{
    Console.WriteLine($"- {award.FirstName}");
}

//feladat 6
var curies = awards.Where(a => a.LastName.Contains("Curie"));
Console.WriteLine("\nCurie díjazottak:");
foreach (var award in curies)
{
    Console.WriteLine($"- {award.FirstName} {award.LastName} {award.Year}");
}

//feladat 7
var howManyAwards = awards.GroupBy(a => a.Type)
                          .Select(g => new { Type = g.Key, Count = g.Count() });

Console.WriteLine("\nDíjak száma típusonként:");
foreach (var item in howManyAwards)
{
    Console.WriteLine($"- {item.Type}: {item.Count} díj");
}

//feladat 8
var medicalAwardsToFile = awards.Where(a => a.Type == "orvosi")
                                    .Select(a => $"{a.Year};{a.FirstName};{a.LastName}");
File.WriteAllLines("orvosi.txt", medicalAwardsToFile);
Console.WriteLine("\norvosi.txt");
var numbers = File.ReadAllLines("hivasok.txt")
                  .Where(x=> x.Length == 9)
                  .Select(x => int.Parse(x))
                  .ToList();

foreach (var number in numbers)
{
    Console.WriteLine(number);
}

var durations = File.ReadAllLines("hivasok.txt")
                     .Where(x => x.Length > 9)
                     .Select(x => TimeSpan.ParseExact(x, "hhmmss", null))
                     .ToList();

foreach (var duration in durations)
{
    Console.WriteLine(duration);
}
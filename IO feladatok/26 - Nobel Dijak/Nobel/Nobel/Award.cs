namespace Nobel;

public class Award
{
    public int Year { get; set; }
    public string Type { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public override string ToString()
    {
        return $"{Year} {Type} - {FirstName} {LastName}";
    }
}

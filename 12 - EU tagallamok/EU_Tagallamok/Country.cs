namespace EU_Tagallamok;

public class Country
{
    public string Name { get; set; }
    public DateTime JoinDate { get; set; }

    public override string ToString()
    {
        return $"{Name};{JoinDate:yyyy.MM.dd}";
    }
}

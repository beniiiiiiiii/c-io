namespace Hivasok;

public class Hivas
{
    public int StartHour { get; set; }
    public int StartMinute { get; set; }
    public int StartSecond { get; set; }

    public int EndHour { get; set; }
    public int EndMinute { get; set; }
    public int EndSecond { get; set; }

    public string PhoneNumber { get; set; }

    public override string ToString()
    {
        return $"{StartHour:D2}:{StartMinute:D2}:{StartSecond:D2} - {EndHour:D2}:{EndMinute:D2}:{EndSecond:D2} : {PhoneNumber}";
    }
}

namespace Snooker;

public class Jatekos
{
    public int Helyezes { get; set; }
    public string Nev { get; set; }
    public string Orszag { get; set; }
    public int Nyeremeny { get; set; }

    public override string ToString()
    {
        return $"{Helyezes}, {Nev}, {Orszag}, {Nyeremeny}";
    }
}

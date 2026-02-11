namespace Hivasok;

class Hivas
{
    public TimeSpan Kezdet { get; set; }
    public TimeSpan Vege { get; set; }
    public string Telefonszam { get; set; }

    public int SzamlazottPercek()
    {
        double percek = (Vege - Kezdet).TotalMinutes;
        return (int)Math.Ceiling(percek);
    }

    public bool Csucsido()
    {
        TimeSpan csucsStart = new TimeSpan(7, 0, 0);
        TimeSpan csucsVege = new TimeSpan(18, 0, 0);

        return Kezdet >= csucsStart && Kezdet < csucsVege;
    }

    public bool Mobil()
    {
        return Telefonszam.StartsWith("39") ||
               Telefonszam.StartsWith("41") ||
               Telefonszam.StartsWith("71");
    }
}

public class Stadion
{
    public string Varos { get; set; }
    public string Nev1 { get; set; }
    public string Nev2 { get; set; }
    public int Ferohely { get; set; }

    public Stadion(string varos, string nev1, string nev2, int ferohely)
    {
        Varos = varos;
        Nev1 = nev1;
        Nev2 = nev2;
        Ferohely = ferohely;
    }
}


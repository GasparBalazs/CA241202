namespace CA241202;

internal class Author
{
    public string VezetekNev { get; private set; }
    public string KeresztNev { get; private set; }
    public Guid Id { get; private set; }

    public Author(string nev)
    {
        var nevek = nev.Split(' ');
        if (nevek.Length != 2 || nevek[0].Length < 3 || nevek[0].Length > 32 || nevek[1].Length < 3 || nevek[1].Length > 32)
        {
            throw new ArgumentException("Helytelen névformátum");
        }
        VezetekNev = nevek[0];
        KeresztNev = nevek[1];
        Id = Guid.NewGuid();
    }
}

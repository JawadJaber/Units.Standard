namespace Units.Standard
{
    public interface IAltitude
    {
        double ValueInM { get; set; }
        double ValueInFt { get; set; }

        string Unit { get; set; }
    }
}

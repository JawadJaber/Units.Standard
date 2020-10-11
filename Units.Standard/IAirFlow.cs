namespace Units.Standard
{
    public interface IAirFlow
    {
        double ValueInCFM { get; set; }
        double ValueInCMH { get; set; }
        double ValueInMPS { get; set; }
        double ValueInLPS { get; set; }

        string Unit { get; set; }

    }
}

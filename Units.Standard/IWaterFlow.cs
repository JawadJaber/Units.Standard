namespace Units.Standard
{
    public interface IWaterFlow
    {
        double ValueInGPM { get; set; }
        double ValueInLPS { get; set; }
        double ValueInMPS { get; set; }

        string Unit { get; set; }
    }
}

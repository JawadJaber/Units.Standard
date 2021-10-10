namespace Units.Standard
{
    public interface IPressureDrop
    {
        double ValueInPa { get; set; }
        double ValueInInWG { get; set; }
        double ValueInFtWG { get; set; }
        double ValueInKPa { get; set; }
        double ValueInPSI { get; set; }
        double ValueInBar { get; set; }
        string Unit { get; set; }
    }
}

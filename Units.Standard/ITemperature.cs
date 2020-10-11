namespace Units.Standard
{
    public interface ITemperature
    {
        double ValueInF { get; set; }
        double ValueInC { get; set; }

        string Unit { get; set; }
    }
}

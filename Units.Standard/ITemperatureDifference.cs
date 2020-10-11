namespace Units.Standard
{
    public interface ITemperatureDifference
    {
        double ValueInC { get; set; }
        double ValueInF { get; set; }
        string Unit { get; set; }

    }
}

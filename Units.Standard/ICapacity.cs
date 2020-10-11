namespace Units.Standard
{
    public interface ICapacity
    {
        double ValueInMBH { get; set; }
        double ValueInTR { get; set; }
        double ValueInKW { get; set; }

        string Unit { get; set; }
    }
}

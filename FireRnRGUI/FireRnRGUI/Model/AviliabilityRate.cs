namespace FireRnRGUI.Model
{
    public partial class AviliabilityRate
    {
        public uint RentalId { get; set; }
        public uint PropertyId { get; set; }
        public DateOnly AvailableFrom { get; set; }
        public DateOnly AvailableUntil { get; set; }
        public decimal DailyRate { get; set; }

        public virtual Property Property { get; set; } = null!;
    }
}

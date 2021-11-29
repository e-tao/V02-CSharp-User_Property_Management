namespace FireRnRGUI.Model
{
    public partial class Booking
    {
        public Booking()
        {
            Messages = new HashSet<Message>();
            Ratings = new HashSet<Rating>();
        }

        public uint BookingId { get; set; }
        public uint PropertyId { get; set; }
        /// <summary>
        /// Guest user
        /// </summary>
        public uint UserId { get; set; }
        public DateOnly BookingTime { get; set; }
        public DateOnly BookedFrom { get; set; }
        public DateOnly BookedUntil { get; set; }
        public byte BookingConfirmation { get; set; }
        public byte PaymentConfirmation { get; set; }
        public string? PaymentMethod { get; set; }
        public DateOnly? PaymentDate { get; set; }
        public uint BookingExpires { get; set; }

        public virtual Property Property { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}

namespace FireRnRGUI.Model
{
    public partial class Message
    {
        public uint MessageId { get; set; }
        public uint? BookingId { get; set; }
        public uint SenderId { get; set; }
        public uint ToUserId { get; set; }
        public string MessageContent { get; set; } = null!;
        public DateTime MessageTime { get; set; }

        public virtual Booking? Booking { get; set; }
        public virtual User Sender { get; set; } = null!;
        public virtual User ToUser { get; set; } = null!;
    }
}

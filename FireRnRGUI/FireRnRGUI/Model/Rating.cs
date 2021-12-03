using System;
using System.Collections.Generic;

namespace FireRnRGUI.Model
{
    public partial class Rating
    {
        public uint RatingId { get; set; }
        public uint? UserId { get; set; }
        public uint? BookingId { get; set; }
        public uint UserRate { get; set; }
        public string? UserRateAs { get; set; }
        public string? UserComment { get; set; }

        public virtual Booking? Booking { get; set; }
        public virtual User? User { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace FireRnRGUI.Model
{
    public partial class User
    {
        public User()
        {
            Bookings = new HashSet<Booking>();
            MessageSenders = new HashSet<Message>();
            MessageToUsers = new HashSet<Message>();
            PropertyAmenityOwners = new HashSet<PropertyAmenityOwner>();
            Ratings = new HashSet<Rating>();
        }

        public uint UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string UserFirstName { get; set; } = null!;
        public string UserLastName { get; set; } = null!;
        public string? Gender { get; set; }
        public string MailAddrStreetNo { get; set; } = null!;
        public string MailAddrStreet { get; set; } = null!;
        public string MailAddrCity { get; set; } = null!;
        public string MailAddrProv { get; set; } = null!;
        public string MailAddrCountry { get; set; } = null!;
        public string MailPostalCode { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNo { get; set; }
        public DateOnly DateJoined { get; set; }
        public DateOnly? LastLogin { get; set; }
        public DateOnly? LastReservation { get; set; }
        public decimal AccumulatedRate { get; set; }
        public byte AccountActivation { get; set; }
        public string Photo { get; set; } = null!;

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Message> MessageSenders { get; set; }
        public virtual ICollection<Message> MessageToUsers { get; set; }
        public virtual ICollection<PropertyAmenityOwner> PropertyAmenityOwners { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}

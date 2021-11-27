using System;
using System.Collections.Generic;

namespace FireRnRGUI.Model
{
    public partial class Property
    {
        public Property()
        {
            AviliabilityRates = new HashSet<AviliabilityRate>();
            Bookings = new HashSet<Booking>();
            PropertyAmenityOwners = new HashSet<PropertyAmenityOwner>();
        }

        public uint PropertyId { get; set; }
        public string PropertyName { get; set; } = null!;
        public string PropertyAddrNo { get; set; } = null!;
        public string PropertyAddrStreet { get; set; } = null!;
        public string PropertyAddrCity { get; set; } = null!;
        public string PropertyAddrProv { get; set; } = null!;
        public string PropertyAddrCountry { get; set; } = null!;
        public string PropertyPostalCode { get; set; } = null!;
        public string PropertyType { get; set; } = null!;
        public string? PropertyDescription { get; set; }
        public string? PropertyPhoto { get; set; }
        public uint NoOfRooms { get; set; }

        public virtual ICollection<AviliabilityRate> AviliabilityRates { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<PropertyAmenityOwner> PropertyAmenityOwners { get; set; }
    }
}

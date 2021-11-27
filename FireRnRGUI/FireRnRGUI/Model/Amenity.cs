using System;
using System.Collections.Generic;

namespace FireRnRGUI.Model
{
    public partial class Amenity
    {
        public Amenity()
        {
            PropertyAmenityOwners = new HashSet<PropertyAmenityOwner>();
        }

        public uint AmenityId { get; set; }
        public string? Amenity1 { get; set; }

        public virtual ICollection<PropertyAmenityOwner> PropertyAmenityOwners { get; set; }
    }
}

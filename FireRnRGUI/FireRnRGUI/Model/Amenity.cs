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


        public override string ToString()
        {
            return Amenity1;
        }
    }
}

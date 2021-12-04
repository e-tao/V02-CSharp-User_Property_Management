namespace FireRnRGUI.Model
{
    /// <summary>
    /// Junction table
    /// </summary>
    public partial class PropertyAmenityOwner
    {
        public uint PropertyAmenityId { get; set; }
        public uint AmenityId { get; set; }
        public uint PropertyId { get; set; }
        /// <summary>
        /// Owner of the property
        /// </summary>
        public uint UserId { get; set; }

        public virtual Amenity Amenity { get; set; } = null!;
        public virtual Property Property { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}

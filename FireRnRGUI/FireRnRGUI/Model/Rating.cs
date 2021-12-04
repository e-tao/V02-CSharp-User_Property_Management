namespace FireRnRGUI.Model
{
    public partial class Rating
    {
        public uint RatingId { get; set; }
        public uint? UserId { get; set; }
        public uint? PropertyId { get; set; }
        public uint UserRate { get; set; }
        public string? UserRateAs { get; set; }
        public string? UserComment { get; set; }

        public virtual User? User { get; set; }


        public override string ToString()
        {
            return UserRate.ToString();
        }


    }
}

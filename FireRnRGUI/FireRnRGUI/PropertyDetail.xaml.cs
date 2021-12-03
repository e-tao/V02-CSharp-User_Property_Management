using System.Diagnostics;

namespace FireRnRGUI
{

    public partial class PropertyDetail : Window
    {
        private Property property;
        private User loginUser;
        private IEnumerable<Amenity> amenities;
        private List<Rating> ratingsList;


        private int rating;

        public PropertyDetail(Property property, User loginUser, IEnumerable<Amenity> amenities)
        {
            InitializeComponent();
            this.property = property;
            this.loginUser = loginUser;
            this.amenities = amenities;
            PropertyGrid.DataContext = property;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using var db = new FirernrContext();
            ratingsList = db.Ratings.ToList();

            string selectedPropertyAmentity = "";

            foreach (var amenity in amenities)
            {
                selectedPropertyAmentity += amenity.ToString() + ", ";
            }

            Amenities.Text = selectedPropertyAmentity;

            OverallRating.Value =OverallRate();
        }

        private void BasicRatingBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            rating = RatingBar.Value;
        }


        private int OverallRate()
        {
            var allRatingsList = ratingsList.Where(p => p.PropertyId==property.PropertyId && !p.UserRateAs.Equals("OWNER")).ToList();
            uint totalRating = 0;
            foreach(var rate in allRatingsList)
            {
                totalRating += uint.Parse(rate.ToString());
            }

           int overAllRating=allRatingsList.Count > 0 ? (int)(totalRating / allRatingsList.Count) : 0;

            return overAllRating;
        }

        private void BtnSaveReview_Click(object sender, RoutedEventArgs e)
        {
            using var db = new FirernrContext();
            var userOrGuest = loginUser.UserId==null ? "GUEST" : "USER";

            var newRating = new Rating()
            {
                UserId = loginUser.UserId,
                UserComment = Review.Text,
                UserRateAs = userOrGuest,
                UserRate = (uint)rating,
                PropertyId = property.PropertyId
            };

            db.Ratings.Add(newRating);
            db.SaveChanges();
        }
    }
}

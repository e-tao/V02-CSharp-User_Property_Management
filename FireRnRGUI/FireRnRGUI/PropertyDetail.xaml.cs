using System.Diagnostics;

namespace FireRnRGUI
{

    public partial class PropertyDetail : Window
    {
        private Property property;
        private User loginUser;
        private IEnumerable <Amenity> amenities;
        private List<Rating> ratingsList;
        private List<Booking> bookingsList;

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

            string selectedPropertyAmentity="";

            foreach (var amenity in amenities)
            {
                selectedPropertyAmentity += amenity.ToString() + ", ";
            }

            Amenities.Text = selectedPropertyAmentity;
        }

        private void BasicRatingBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            rating = RatingBar.Value;
          
        }

        private void BtnSaveReview_Click(object sender, RoutedEventArgs e)
        {
            var userOrGuest = loginUser.UserId==null ? null : "USER";

            var ratedProperty = proper




            var newRating = new Rating()
            {
                UserId = loginUser.UserId,
                UserComment = Review.Text,
                UserRateAs = userOrGuest,
                UserRate = (uint)rating
            };



            Debug.WriteLine(rating);
        }
    }
}

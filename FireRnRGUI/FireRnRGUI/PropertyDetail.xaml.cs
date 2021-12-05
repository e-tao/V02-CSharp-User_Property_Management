namespace FireRnRGUI
{

    public partial class PropertyDetail : Window
    {
        private Property property;
        private User loginUser;
        private IEnumerable<Amenity> amenities;
        private IEnumerable<Rating> relatedRating;
        private IEnumerable<Rating> relatedRatingAndUser;
        private List<Rating> ratingsList;
        private int rating;
        private Rating selectedRating;
        private List<User> userList;


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
            userList = db.Users.ToList();

            string selectedPropertyAmentity = "";
            foreach (var amenity in amenities)
            {
                selectedPropertyAmentity += amenity.ToString() + ", ";
            }
            Amenities.Text = selectedPropertyAmentity;


            relatedRating = ratingsList.Where(p => p.PropertyId == property.PropertyId && p.UserRateAs.Equals("USER")).OrderByDescending(r=>r.RatingId);
            relatedRatingAndUser = relatedRating.Where(p => userList.Any(u => u.UserId == p.UserId));
            ReviewList.ItemsSource = relatedRatingAndUser;

            var user = relatedRatingAndUser.Where(u => u.UserId == loginUser.UserId);
            if(user.Any())
            {
                Review.Text = "You already reviewed this property, \rHave more to share? Please update the review. \rThanks for using FireRnR service.";
                SaveUpdate.IsEnabled = false;
            }


            LoginUser.Text = loginUser.UserFirstName + " " + loginUser.UserLastName;
            UserLogo.ImageSource = new BitmapImage(new Uri(loginUser.Photo, UriKind.Absolute));

            OverallRating.Value =OverallRate();
        }


        private void BasicRatingBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            rating = RatingBar.Value;
        }


        private int OverallRate()
        {
            var allRatingsList = ratingsList.Where(p => p.PropertyId==property.PropertyId && p.UserRateAs.Equals("USER")).ToList();
            uint totalRating = 0;
            foreach(var rate in allRatingsList)
            {
                totalRating += uint.Parse(rate.ToString());
            }

           int overAllRating=allRatingsList.Count > 0 ? (int)(totalRating / allRatingsList.Count) : 0;

            return overAllRating;
        }

        private void BtnSaveReviewDelete_Click(object sender, RoutedEventArgs e)
        {
            using var db = new FirernrContext();
            Rating newRating=new Rating();
            uint newRatingId;
            if (SaveUpdate.Content.Equals("Save"))
            {
                newRating = new Rating()
                {
                    UserId = loginUser.UserId,
                    UserComment = Review.Text,
                    UserRateAs = "USER",
                    UserRate = (uint)rating,
                    PropertyId = property.PropertyId
                };

                db.Ratings.Add(newRating);
                Reset();
                db.SaveChanges();
            }
            else if (sender.Equals(Delete))
            {   
                db.Ratings.Remove(selectedRating);
                Reset();
                db.SaveChanges();
            }
            else if (SaveUpdate.Content.Equals("Update"))
            {
                selectedRating.UserComment = Review.Text;
                selectedRating.UserRate = (uint)rating;
                db.Ratings.Update(selectedRating);
                Reset();
                db.SaveChanges();
            } 

            Window_Loaded(this, e);
        }

        private void Reset()
        {
            Review.Text = null;
            RatingBar.Value = 0;
            SaveUpdate.Content = "Save";
        }


        private void Card_MouseDown(object sender, MouseButtonEventArgs e)
        {
            selectedRating = (Rating)ReviewList.SelectedItem;
            if (selectedRating.UserId == loginUser.UserId)
            {
                Review.Text = selectedRating.UserComment;
                RatingBar.Value = (int)selectedRating.UserRate;
                SaveUpdate.Content = "Update";
                SaveUpdate.IsEnabled = true;
                Delete.IsEnabled = true;
            }
        }
    }
}

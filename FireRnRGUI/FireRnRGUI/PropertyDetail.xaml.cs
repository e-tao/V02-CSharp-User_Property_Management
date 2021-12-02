using System.Diagnostics;

namespace FireRnRGUI
{

    public partial class PropertyDetail : Window
    {
        private Property property;
        private User loginUser;
        private IEnumerable <Amenity> amenities;

        

    public PropertyDetail(Property property, User loginUser, IEnumerable<Amenity> amenities)
        {
            InitializeComponent();
            this.property = property;
            this.loginUser = loginUser;
            this.amenities = amenities;
            PropertyGrid.DataContext = property;
            //using var db = new FirernrContext();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string selectedPropertyAmentity="";

            foreach (var amenity in amenities)
            {
                selectedPropertyAmentity += amenity.ToString() + ", ";
            }

            Amenities.Text = selectedPropertyAmentity;

            
        }
    }
}

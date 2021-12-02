

using System.Diagnostics;

namespace FireRnRGUI;

public partial class AddProperty : Window
{

    private User loginUser { get; set; }
    private List<Amenity> amenityList;
    private List<Property> propertyList;
    List<string> selectedAmenities = new();

    public AddProperty(User loginUser)
    {
        InitializeComponent();
        this.loginUser = loginUser;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        using var db = new FirernrContext();
        amenityList = db.Amenities.ToList();
        propertyList = db.Properties.ToList();
        AmenityOptions.ItemsSource = amenityList;
        Owner.Text = $"{loginUser.UserFirstName} {loginUser.UserLastName} ({loginUser.UserName})";
    }

    private void BtnSaveProperty_Click(object sender, RoutedEventArgs e)
    {
        var newProperty = new Property()
        {
            PropertyName = PropertyName.Text,
            PropertyAddrNo = PropertyAddrNo.Text,
            PropertyAddrStreet = PropertyAddrStreet.Text,
            PropertyAddrCity = PropertyAddrCity.Text,
            PropertyAddrProv = PropertyAddrProv.Text,
            PropertyAddrCountry = PropertyAddrCountry.Text,
            PropertyPostalCode = PropertyAddrPostalCode.Text,
            NoOfRooms = uint.Parse(NoOfRooms.Text),
            PropertyType = PropertyType.SelectedValue.ToString(),
            PropertyDescription = PropertyDescription.Text,
            PropertyPhoto= PropertyPhoto.Text,

            PropertyAmenityOwners = new List<PropertyAmenityOwner>()
            {
                new PropertyAmenityOwner
                {
                    UserId = loginUser.UserId,
                    AmenityId = 1
                }
            }

        };

        using var db = new FirernrContext();
        db.Properties.Add(newProperty);
        db.SaveChanges();
    }

    private void CheckBox_Checked(object sender, RoutedEventArgs e)
    {
        

        CheckBox cb = sender as CheckBox;
        
        if (cb!=null)
        {
            selectedAmenities.Add(cb.Content.ToString());
        }

    }
}


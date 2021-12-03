namespace FireRnRGUI;

public partial class AddProperty : Window
{

    private User loginUser { get; set; }
    private List<Amenity> amenityList;
    private List<Property> propertyList;
    IEnumerable<Amenity> selectedAmenities;
    List<string> checkBoxAmenitiesSelection;
    MainWindow mainWindow { get; set; }



    public AddProperty(User loginUser, MainWindow mainWindow)
    {
        InitializeComponent();
        this.loginUser = loginUser;
        this.mainWindow = mainWindow;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        using var db = new FirernrContext();
        amenityList = db.Amenities.ToList();
        AmenityOptions.ItemsSource = amenityList;
        Owner.Text = $"{loginUser.UserFirstName} {loginUser.UserLastName} ({loginUser.UserName})";
        checkBoxAmenitiesSelection = new();
    }

    private void BtnSaveProperty_Click(object sender, RoutedEventArgs e)
    {
        using var db = new FirernrContext();
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
            PropertyPhoto = PropertyPhoto.Text
        };
        db.Properties.Add(newProperty);
        db.SaveChanges();

        uint newPropertyId = newProperty.PropertyId;

        foreach (var amenity in selectedAmenities)
        {
            var newPropertyAmenityOwner = new PropertyAmenityOwner()
            {
                UserId = loginUser.UserId,
                AmenityId = amenity.AmenityId,
                PropertyId = newPropertyId
            };

            db.PropertyAmenityOwners.Add(newPropertyAmenityOwner);
        };
        db.SaveChanges();
        
        this.Close();
    }



    private void CheckBox_Checked(object sender, RoutedEventArgs e)
    {
        CheckBox cb = sender as CheckBox;

        if (cb != null)
        {
            checkBoxAmenitiesSelection.Add(cb.Content.ToString());
        }

        selectedAmenities = amenityList.Where(a => checkBoxAmenitiesSelection.Any(item => item.Equals(a.ToString())));
    }

    private void Window_Closed(object sender, EventArgs e)
    {
        mainWindow.PropertyListGrid.ItemsSource = propertyList;
        mainWindow.ReloadAll();
    }
}
global using FireRnRGUI.Model;
global using Microsoft.EntityFrameworkCore;
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Windows;
global using System.Windows.Controls;
global using System.Windows.Input;
global using System.Windows.Media.Imaging;

namespace FireRnRGUI
{

    public partial class MainWindow : Window
    {
        private User loginUser { get; set; }
        private List<User> userList;
        private List<Property> propertyList;
        private List<Amenity> amenityList;
        private List<PropertyAmenityOwner> propertyAmenityOwnersList;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void ReloadAll()
        {
            using var db = new FirernrContext();
            userList = db.Users.ToList();
            propertyList = db.Properties.ToList();
            amenityList = db.Amenities.ToList();
            propertyAmenityOwnersList = db.PropertyAmenityOwners.ToList();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ReloadAll();
            BtnUser_Click(this, e);
        }

        private void BtnUser_Click(object sender, RoutedEventArgs e)
        {
            UserListGrid.ItemsSource = userList;
            UserListGrid.Visibility = Visibility.Visible;

            AddUserGrid.Visibility = Visibility.Hidden;
            PropertyListGrid.Visibility = Visibility.Hidden;
            AmenityListFilter.Visibility = Visibility.Hidden;
        }

        private void PackIconLogin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var selecteduser = (User)UserListGrid.SelectedItem;
            if (selecteduser != null)
            {
                loginUser = selecteduser;
                First.Text = loginUser.UserFirstName;
                Last.Text = loginUser.UserLastName;
                UserLogo.ImageSource = new BitmapImage(new Uri(loginUser.Photo, UriKind.Absolute));
                WelcomeUser.Text = loginUser.UserFirstName + " " + loginUser.UserLastName;
                WelcomeTitle.Visibility = Visibility.Visible;
                UserListGrid.Visibility = Visibility.Hidden;
                BtnProperty_Click(this, e);
            }
            else
            {
                MessageBox.Show("No user is selected!", "Error", MessageBoxButton.OK);
            }
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUserGrid.Visibility = Visibility.Visible;
            UserListGrid.Visibility = Visibility.Hidden;
            PropertyListGrid.Visibility = Visibility.Hidden;
        }

        private void BtnSaveUser_Click(object sender, RoutedEventArgs e)
        {
            var newUser = new User
            {
                UserName = UserName.Text,
                UserFirstName = FirstName.Text,
                UserLastName = LastName.Text,
                Gender = Gender.SelectedValue.ToString(),
                Photo = Photo.Text,
                MailAddrStreetNo = StreetNo.Text,
                MailAddrStreet = Street.Text,
                MailAddrCity = City.Text,
                MailAddrProv = Province.Text,
                MailAddrCountry = Country.Text,
                MailPostalCode = PostalCode.Text,
                Email = Email.Text,
                PhoneNo = Phone.Text
            };

            using var db = new FirernrContext();
            db.Users.Add(newUser);
            db.SaveChanges();
            ReloadAll();
        }

        private void BtnProperty_Click(object sender, RoutedEventArgs e)
        {
            PropertyListGrid.ItemsSource = propertyList;
            AmenityListFilter.ItemsSource = amenityList;

            AmenityListFilter.Visibility = Visibility.Visible;
            PropertyListGrid.Visibility = Visibility.Visible;
            BtnProperty.Visibility = Visibility.Visible;
            BtnAddProperty.Visibility = Visibility.Visible;

            UserListGrid.Visibility = Visibility.Hidden;
            AddUserGrid.Visibility = Visibility.Hidden;

            BtnAddUser.IsEnabled = false;
        }

        private void PropertyList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var property = (Property)PropertyListGrid.SelectedItem;
            var selectedProperty = propertyAmenityOwnersList.Where(p => p.PropertyId == property.PropertyId).ToList();
            var selectedPropertyAmenity = amenityList.Where(a => selectedProperty.Any(p => p.AmenityId == a.AmenityId));
            var propertyWindow = new PropertyDetail(property, loginUser, selectedPropertyAmenity);
            propertyWindow.Show();
        }

        private void AmenityList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedAmenity = AmenityListFilter.SelectedValue as Amenity;
            var filter = propertyAmenityOwnersList.Where(p => p.Amenity == selectedAmenity).ToList();
            var filterList = propertyList.Where(property => filter.Any(f => f.PropertyId == property.PropertyId));
            PropertyListGrid.ItemsSource = filterList;
        }

        private void BtnAddProperty_Click(object sender, RoutedEventArgs e)
        {
            UserListGrid.Visibility = Visibility.Hidden;
            AddUserGrid.Visibility = Visibility.Hidden;
            PropertyListGrid.Visibility = Visibility.Hidden;
            AmenityListFilter.Visibility = Visibility.Hidden;
            var addPropertyWindow = new AddProperty(loginUser, this);
            addPropertyWindow.ShowDialog();
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            loginUser = null;
            First.Text = null;
            Last.Text = null;
            UserLogo.ImageSource = null;
            WelcomeUser.Text = null;
            WelcomeTitle.Visibility = Visibility.Hidden;

            BtnProperty.Visibility = Visibility.Hidden;
            BtnAddProperty.Visibility = Visibility.Hidden;
            PropertyListGrid.Visibility = Visibility.Hidden;
            AmenityListFilter.Visibility = Visibility.Hidden;

            UserListGrid.Visibility = Visibility.Visible;
            BtnAddUser.IsEnabled = true;
            //BtnAddUser.Visibility = Visibility.Visible;
        }
    }
}

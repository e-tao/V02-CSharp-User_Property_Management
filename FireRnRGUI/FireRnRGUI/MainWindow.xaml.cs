global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Windows;
global using FireRnRGUI.Model;
global using System.Windows.Media.Imaging;
global using System.Windows.Input;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata;

using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace FireRnRGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using var db = new FirernrContext();
            userList = db.Users.ToList();
            propertyList = db.Properties.ToList();
            amenityList = db.Amenities.ToList();
            propertyAmenityOwnersList = db.PropertyAmenityOwners.ToList();
            BtnUser_Click(this, e);
        }

        private void BtnUser_Click(object sender, RoutedEventArgs e)
        {
            BtnUserList.ItemsSource = userList;
            BtnUserList.Visibility = Visibility.Visible;
            AddUser.Visibility = Visibility.Hidden;
            PropertyList.Visibility = Visibility.Hidden;
        }


        private void PackIconLogin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var selecteduser = (User)BtnUserList.SelectedItem;
            if(selecteduser != null)
            {
                loginUser = selecteduser;
                First.Text = loginUser.UserFirstName;
                Last.Text = loginUser.UserLastName;
                UserLogo.ImageSource = new BitmapImage(new Uri(loginUser.Photo, UriKind.Absolute));
                WelcomeUser.Text = loginUser.UserFirstName + " " + loginUser.UserLastName;
                WelcomeTitle.Visibility = Visibility.Visible;
                BtnUserList.Visibility = Visibility.Hidden;
                BtnProperty_Click(this, e);
                BtnProperty.Visibility = Visibility.Visible;
                BtnAddProperty.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("No user is selected!", "Error", MessageBoxButton.OK);
            }
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUser.Visibility = Visibility.Visible;
            BtnUserList.Visibility = Visibility.Hidden;
            PropertyList.Visibility= Visibility.Hidden;
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
            Window_Loaded(this, e);
        }

        private void BtnProperty_Click(object sender, RoutedEventArgs e)
        {
            PropertyList.ItemsSource = propertyList;
            AmenityList.ItemsSource = amenityList;
            AmenityList.Visibility = Visibility.Visible;
            PropertyList.Visibility = Visibility.Visible;
            BtnUserList.Visibility = Visibility.Hidden;
            AddUser.Visibility = Visibility.Hidden;
        }

        private void PropertyList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var property = (Property)PropertyList.SelectedItem;
            var propertyWindow = new PropertyDetail(property, loginUser);
            propertyWindow.Show();
        }

        private void AmenityList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedAmenity = AmenityList.SelectedValue as Amenity;
            var resultList = propertyAmenityOwnersList.Where(p=>p.Amenity == selectedAmenity).ToList();
            PropertyList.ItemsSource = resultList;


        }
    }
}

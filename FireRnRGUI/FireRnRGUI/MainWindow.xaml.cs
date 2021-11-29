global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Windows;
global using FireRnRGUI.Model;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata;



using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace FireRnRGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private User loginUser { get; set; }
        private List<User> userList;
        

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UserList.Visibility = Visibility.Hidden;
            using var db = new FirernrContext();
            userList = db.Users.ToList();
            BtnUser_Click(this, e);
        }

        private void BtnUser_Click(object sender, RoutedEventArgs e)
        {
            UserList.ItemsSource = userList;
            UserList.Visibility = Visibility.Visible;
            AddUser.Visibility = Visibility.Hidden;
        }


        private void PackIconLogin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var selecteduser = (User)UserList.SelectedItem;
            if(selecteduser != null)
            {
                loginUser = selecteduser;
                First.Text = loginUser.UserFirstName;
                Last.Text = loginUser.UserLastName;
                UserLogo.ImageSource = new BitmapImage(new Uri(loginUser.Photo, UriKind.Absolute));
                WelcomeUser.Text = loginUser.UserFirstName + " " + loginUser.UserLastName;
                WelcomeTitle.Visibility = Visibility.Visible;
                Property.Visibility = Visibility.Visible;
                AddProperty.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("No user is selected!", "Error", MessageBoxButton.OK);
            }
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUser.Visibility = Visibility.Visible;
            UserList.Visibility = Visibility.Hidden;
        }
    }
}

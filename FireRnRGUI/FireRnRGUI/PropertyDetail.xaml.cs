namespace FireRnRGUI
{

    public partial class PropertyDetail : Window
    {
        private Property property;
        private User loginUser;


        public PropertyDetail(Property property, User loginUser)
        {
            this.property = property;
            this.loginUser = loginUser;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PropertyTitle.Text = property.PropertyName;
        }
    }
}

namespace FireRnRGUI
{

    public partial class PropertyDetail : Window
    {
        private Property property;
        private User loginUser;

        

    public PropertyDetail(Property property, User loginUser)
        {
            InitializeComponent();
            this.property = property;
            this.loginUser = loginUser;
            PropertyGrid.DataContext = property;
            using var db = new FirernrContext();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PropertyTitle.Text = property.PropertyName;
          
        }
    }
}

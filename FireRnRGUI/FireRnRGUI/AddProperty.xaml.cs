namespace FireRnRGUI;

public partial class AddProperty : Window
{

    private User loginUser { get; set; }

    public AddProperty(User loginUser)
    {
        InitializeComponent();
        this.loginUser = loginUser;
    }
}


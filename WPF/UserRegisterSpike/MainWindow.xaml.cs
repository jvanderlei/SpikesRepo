using System.Windows;

namespace UserRegisterSpike
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DBService dbService = new();

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            

        }

        private void Register_Button_Click(object sender, RoutedEventArgs e)
        {
            dbService.InsertUser("João", "12345678");
        }


    }
}
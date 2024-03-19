using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using Views;

namespace Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void UsernameGotFocus(object sender, RoutedEventArgs e)
        {
            if (Username.Text == "Username")
            {
                Username.Text = "";
                Username.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void UsernameLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Username.Text))
            {
                Username.Text = "Username";
                Username.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            string username = Username.Text;
            string password = PasswordBox.Password;

            // Open Dashboard Window
            DashboardWindow dashboard = new DashboardWindow();
            dashboard.Show();
            this.Close();

        }
        private bool AuthenticateUser(string username, string password)
        {
            return false;
        }

        private void OtherOptionsMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MessageBox.Show("Other options clicked!");
        }

    }
}

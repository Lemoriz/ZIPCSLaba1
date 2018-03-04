using ClassLibaryForLabOne;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MabyFinal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            Authoriz authoriz = new Authoriz("authorization.txt");

            User user = authoriz.Authorization(login.Text, password.Text);

            if (user == null)
            {
                MessageBox.Show("The username or password you entered is incorrect", "Error");
            }
            else
            {
                MessageBox.Show("Welcome, " + user.Login);

                Information information = new Information(user, authoriz);
                information.Owner = this;

                information.Show();

                this.Hide();
            }
        }
    }
}

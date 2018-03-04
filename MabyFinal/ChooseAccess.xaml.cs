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
using System.Windows.Shapes;

namespace MabyFinal
{
    /// <summary>
    /// Interaction logic for ChooseAccess.xaml
    /// </summary>
    public partial class ChooseAccess : Window
    {
        public string nameObject;
        public User user;
        public Authoriz authoriz;

        public ChooseAccess(User user, string nameObject, Authoriz authoriz)
        {
            InitializeComponent();
            this.nameObject = nameObject;
            this.user = user;
            this.authoriz = authoriz;

            objectName.Text = this.nameObject;
        }


        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            var asd = new ActionCode();

            switch (ComboBox.Text)
            {
                case "Write":
                    asd = ActionCode.write;
                    break;
                case "Read":
                    asd = ActionCode.read;
                    break;
                case "Delete":
                    asd = ActionCode.delete;
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }

            var asdd = authoriz.CheckAccessForAction(user, nameObject, asd);
            if (asdd)
            {
                MessageBox.Show("Action completed", "Successfully");
            }
            else
            {
                MessageBox.Show("You don't have access", "Error!");
            }
        }
    }
}

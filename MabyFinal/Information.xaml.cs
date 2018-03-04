using ClassLibaryForLabOne;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MabyFinal
{
    /// <summary>
    /// Interaction logic for Information.xaml
    /// </summary>
    ///

    public class AccessLevelForUser
    {
        public string nameObject { get; set; }
        public string nameAccess { get; set; }
    }

    public class AllUsersInformation
    {
        public string Login { get; set; }
        public bool SuperUser { get; set; }
        public string ObjectName1 { get; set; }
        public string ObjectAccess1 { get; set; }
        public string ObjectName2 { get; set; }
        public string ObjectAccess2 { get; set; }
        public string ObjectName3 { get; set; }
        public string ObjectAccess3 { get; set; }
    }


    public partial class Information : Window
    {
        User user;
        Authoriz authoriz;
        List<User> allUsers;
        int index;

        public Information(User user, Authoriz authoriz)
        {
            InitializeComponent();

            this.user = user;
            this.authoriz = authoriz;
            userName.Text = user.Login;

            string[] list = JsonTools.DeserializeFromFile<JArray>("namesFile.txt").ToObject<string[]>();

            List<string> asd = new List<string>();

            var info = authoriz.CheckAccessUsers(user, list);

            foreach (var item in info)
            {
                AccessLevelForUser dic = new AccessLevelForUser();
                dic.nameObject = item.Key;
                dic.nameAccess = item.Value.ToString();

                grida.Items.Add(dic);
            }
            grida.IsReadOnly = true;

             allUsers = authoriz.GetDataAllUsers();

            ObservableCollection<AllUsersInformation> allUsersInformation1 = new ObservableCollection<AllUsersInformation>();

            foreach (var item in allUsers)
            {
                AllUsersInformation allUsersInformation = new AllUsersInformation();

                allUsersInformation.Login = item.Login;
                allUsersInformation.SuperUser = item.SuperUser;

                for (int i = 0; i < info.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            allUsersInformation.ObjectName1 = item.Access.ElementAt(i).Key;
                            allUsersInformation.ObjectAccess1 = item.Access.ElementAt(i).Value.ToString();
                            break;
                        case 1:
                            allUsersInformation.ObjectName2 = item.Access.ElementAt(i).Key;
                            allUsersInformation.ObjectAccess2 = item.Access.ElementAt(i).Value.ToString();
                            break;
                        case 2:
                            allUsersInformation.ObjectName3 = item.Access.ElementAt(i).Key;
                            allUsersInformation.ObjectAccess3 = item.Access.ElementAt(i).Value.ToString();
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }

                }
                allUsersInformation1.Add(allUsersInformation);
            }
            grida2.ItemsSource = allUsersInformation1;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MainWindow window3 = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                if (window3 != null)
                {
                    window3.password.Text = "";
                    window3.login.Text = "";
                    window3.Show();
                }
            });
        }

        private int FindRowIndex(DataGridRow row)
        {
            DataGrid dataGrid = ItemsControl.ItemsControlFromItemContainer(row) as DataGrid;

            int index = dataGrid.ItemContainerGenerator.IndexFromContainer(row);

            return index;
        }

        private object ExtractBoundValue(DataGridRow row, DataGridCell cell)
        {
            // find the property that this cell's column is bound to
            string boundPropertyName = FindBoundProperty(cell.Column);

            // find the object that is realted to this row
            object data = row.Item;

            // extract the property value
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(data);
            PropertyDescriptor property = properties[boundPropertyName];

            if (property != null)
            {
                object value = property.GetValue(data);
                return value;
            }
            else
            {
                object value = " ";
                return value;
            }
        }

        private string FindBoundProperty(DataGridColumn col)
        {
            DataGridBoundColumn boundColumn = col as DataGridBoundColumn;

            // find the property that this column is bound to
            Binding binding = boundColumn.Binding as Binding;
            string boundPropertyName = binding.Path.Path;

            return boundPropertyName;
        }

        private void grida_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;

            while ((dep != null) && !(dep is DataGridCell) && !(dep is DataGridColumnHeader))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            if (dep is DataGridColumnHeader)
            {
                DataGridColumnHeader columnHeader = dep as DataGridColumnHeader;

                // find the property that this cell's column is bound to
                string boundPropertyName = FindBoundProperty(columnHeader.Column);

                int columnIndex = columnHeader.Column.DisplayIndex;

                ClickedItemDisplay.Text = string.Format(
                    "Header clicked [{0}] = {1}",
                    columnIndex, boundPropertyName);
            }

            if (dep is DataGridCell)
            {
                DataGridCell cell = dep as DataGridCell;

                // navigate further up the tree
                while ((dep != null) && !(dep is DataGridRow))
                {
                    dep = VisualTreeHelper.GetParent(dep);
                }

                if (dep == null)
                    return;

                DataGridRow row = dep as DataGridRow;

                object value = ExtractBoundValue(row, cell);

                int columnIndex = cell.Column.DisplayIndex;
                int rowIndex = FindRowIndex(row);

                if (columnIndex == 0)
                {
                    ChooseAccess information = new ChooseAccess(user, value.ToString(), authoriz);
                    information.Owner = this;

                    information.ShowDialog();
                }
            }
        }


        private void grida2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;

            while ((dep != null) && !(dep is DataGridCell) && !(dep is DataGridColumnHeader))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            if (dep is DataGridColumnHeader)
            {
                DataGridColumnHeader columnHeader = dep as DataGridColumnHeader;

                // find the property that this cell's column is bound to
                string boundPropertyName = FindBoundProperty(columnHeader.Column);

                int columnIndex = columnHeader.Column.DisplayIndex;

                ClickedItemDisplay.Text = string.Format(
                    "Header clicked [{0}] = {1}",
                    columnIndex, boundPropertyName);
            }

            if (dep is DataGridCell)
            {
                DataGridCell cell = dep as DataGridCell;

                // navigate further up the tree
                while ((dep != null) && !(dep is DataGridRow))
                {
                    dep = VisualTreeHelper.GetParent(dep);
                }

                if (dep == null)
                    return;

                int columnIndex = cell.Column.DisplayIndex;

                index = columnIndex;

                DataGridRow row = dep as DataGridRow;
                {
                    object value = ExtractBoundValue(row, cell);

                    columnIndex = cell.Column.DisplayIndex;
                    int rowIndex = FindRowIndex(row);
                }
            }
        }

        private void grida2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.F5))
            {
                AllUsersInformation allUsersInformation = grida2.SelectedItem as AllUsersInformation;

                foreach (var item in allUsers)
                {
                    if (item.Login == allUsersInformation.Login)
                    {
                        switch (index)
                        {
                            case 3:
                                if (authoriz.ChangeAccessForSomeUser(user, item, allUsersInformation.ObjectName1, Convert.ToInt32(allUsersInformation.ObjectAccess1)))
                                {
                                    MessageBox.Show("Changed access to the object: " + allUsersInformation.ObjectName1, "Success");
                                }
                                else
                                {
                                    MessageBox.Show("You don't have access", "Error!");
                                }
                                break;
                            case 5:
                                if(authoriz.ChangeAccessForSomeUser(user, item, allUsersInformation.ObjectName2, Convert.ToInt32(allUsersInformation.ObjectAccess2)))
                                {
                                    MessageBox.Show("Changed access to the object: " + allUsersInformation.ObjectName2, "Success");
                                }
                                else
                                {
                                    MessageBox.Show("You don't have access", "Error!");
                                }
                                break;
                            case 7:
                                if(authoriz.ChangeAccessForSomeUser(user, item, allUsersInformation.ObjectName3, Convert.ToInt32(allUsersInformation.ObjectAccess3)))
                                {
                                    MessageBox.Show("Changed access to the object: " + allUsersInformation.ObjectName3, "Success");
                                }
                                else
                                {
                                    MessageBox.Show("You don't have access", "Error!");
                                }
                                break;
                            default:
                                Console.WriteLine("Default case");
                                break;
                        }
                    }
                    else
                    {
                    }
                }
            }
               
        }
    }
}

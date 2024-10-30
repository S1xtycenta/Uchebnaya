using System.IO;
using System.Linq;
using System.Windows;
using UchebnayaPractica.Pages;

namespace UchebnayaPractica
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            App.mainWindow = this;
            if (File.Exists(@"RememberMe.txt"))
            {
                string Login = File.ReadAllText(@"RememberMe.txt");
                App.currentUser = App.db.User.FirstOrDefault(x => x.Login == Login);
                if(App.currentUser == null)
                {
                    MainFrame.Navigate(new AuthPage());
                    return;
                }
                if (App.currentUser.RoleId == 3)
                {
                    App.mainWindow.SetIcons(true, true, true, true, true, false, false);
                    MainFrame.Navigate(new EmployeePage());
                }
                else if (App.currentUser.RoleId == 4)
                {
                    App.mainWindow.SetIcons(false, false, true, false, true, false, false);
                    MainFrame.Navigate(new OrderPage());
                }
                else if (App.currentUser.RoleId == 1)
                {
                    App.mainWindow.SetIcons(false, true, true, false, true, true, true);
                    MainFrame.Navigate(new AccessoriesAndMaterialsPage());
                }
                else
                {
                    App.mainWindow.SetIcons(false, true, true, false, true, false, false);
                    MainFrame.Navigate(new AccessoriesAndMaterialsPage());
                }
                Methods.TakeInformation("Вы успешно зашли в систему!");
            }
            else
                MainFrame.Navigate(new AuthPage());
        }

        public void SetIcons(bool employee, bool materials, bool account, bool plan, bool order, bool failure, bool test)
        {
            Employee.Visibility = employee ? Visibility.Visible : Visibility.Collapsed;
            Material.Visibility = materials ? Visibility.Visible : Visibility.Collapsed;
            Person.Visibility = account ? Visibility.Visible : Visibility.Collapsed;
            Plan.Visibility = plan ? Visibility.Visible : Visibility.Collapsed;
            Order.Visibility = order ? Visibility.Visible : Visibility.Collapsed;
            Failure.Visibility = failure ? Visibility.Visible : Visibility.Collapsed;
            Test.Visibility = test ? Visibility.Visible : Visibility.Collapsed;           
        }

        private void Employee_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new EmployeePage());
        }

        private void Plan_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PlanPage());
        }

        private void Material_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AccessoriesAndMaterialsPage());
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new OrderPage());
        }

        private void Failure_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new FailurePage());
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new TestPage());
        }

        private void Person_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AddEditEmployee(App.currentUser, false, "Ваш профиль"));
        }
    }
}

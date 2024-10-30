using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using UchebnayaPractica.Database;

namespace UchebnayaPractica.Pages
{
    /// <summary>
    /// Логика взаимодействия для EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage : Page
    {
        public EmployeePage()
        {
            InitializeComponent();
            SortCb.SelectedIndex = 0;
        }

        public void Refresh()
        {
            IEnumerable<User> employee = App.db.User.Where(x => x.RoleId != 4);

            if(SearchTb.Text != "")
                employee = employee.Where(x => x.FIO.Contains(SearchTb.Text));


            if (SortCb.SelectedIndex == 1)
                employee = employee.OrderBy(x => x.LastName);
            else if (SortCb.SelectedIndex == 2)
                employee = employee.OrderByDescending(x => x.LastName);
            else if (SortCb.SelectedIndex == 3)
                employee = employee.OrderBy(x => x.Age);
            else if (SortCb.SelectedIndex == 4)
                employee = employee.OrderByDescending(x => x.Age);
            else if (SortCb.SelectedIndex == 5)
                employee = employee.OrderBy(x => x.RoleId);


            MyList.ItemsSource = employee.ToList();
        }

        private void AddEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            App.employeePage = this;
            NavigationService.Navigate(new AddEditEmployee(new User(), true));
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }

        private void SortCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            App.employeePage = this;
            NavigationService.Navigate(new AddEditEmployee((sender as Button).DataContext as User, false, "Редактировать сотрудника"));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (Methods.TakeChoice("Вы точно хотите удалить сотрудника?"))
            {
                if (((sender as Button).DataContext as User).Login == App.currentUser.Login)
                {
                    Methods.TakeWarning("Вы не можете уждалить самого себя!");
                    return;
                }
                App.db.User.Remove((sender as Button).DataContext as User);
                Methods.TakeInformation("Успешно удалено!");
            }
        }
    }
}

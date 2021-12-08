using Employees.Desktop.Models;
using System.Globalization;
using System.Text;
using System.Windows;

namespace Employees.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void GetEmployeesResponse(string search = "")
        {
            employeesGrid.ItemsSource = APICommunication.GetEmployees(out string msg, search);
            if (msg != "OK")
            {
                MessageBox.Show(msg);
            }
        }

        internal void GetPositionsResponse()
        {
            cbPosition.ItemsSource = APICommunication.GetPositions(out string msg);
            if (msg != "OK")
            {
                MessageBox.Show(msg);
            }
        }

        internal void GetDepartmentsResponse()
        {
            cbDepartment.ItemsSource = APICommunication.GetDepartments(out string msg);
            if (msg != "OK")
            {
                MessageBox.Show(msg);
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            GetEmployeesResponse();
            GetPositionsResponse();
            GetDepartmentsResponse();
        }

        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new();

            if (!decimal.TryParse(tbSalary.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal salary))
            {
                errors.AppendLine("Зарплата может быть только числом");
            }
            if (string.IsNullOrEmpty(tbLastName.Text) || string.IsNullOrWhiteSpace(tbLastName.Text))
            {
                errors.AppendLine("Заполните поле Фамилия");
            }
            if (string.IsNullOrEmpty(tbName.Text) || string.IsNullOrWhiteSpace(tbName.Text))
            {
                errors.AppendLine("Заполните поле Имя");
            }
            if (string.IsNullOrWhiteSpace(tbPhone.Text) || string.IsNullOrEmpty(tbPhone.Text))
            {
                errors.AppendLine("Заполните поле Телефон");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            var idDepartment = (cbDepartment.SelectedItem as Department).Id;
            var idPosition = (cbPosition.SelectedItem as Position).Id;
            var newEmployee = new Employee
            {
                LastName = tbLastName.Text,
                FirstName = tbName.Text,
                Patronymic = tbPatronymic.Text,
                Phone = tbPhone.Text,
                Salary = salary,
                DepartmentId = idDepartment,
                PositionId = idPosition
            };
            APICommunication.PostEmployee(newEmployee, out var response);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Сотрудник успешно добавлен");
                tbLastName.Text = string.Empty;
                tbName.Text = string.Empty;
                tbPatronymic.Text = string.Empty;
                tbPhone.Text = string.Empty;
                tbSalary.Text = string.Empty;
                cbDepartment.SelectedIndex = 0;
                cbPosition.SelectedIndex = 0;
                GetEmployeesResponse();
            }
            else
            {
                MessageBox.Show(response.ReasonPhrase);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (employeesGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите сотрудника в таблице");
                return;
            }
            var window = new EditEmployeeWindow(employeesGrid.SelectedItem as Employee);
            window.ShowDialog();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (employeesGrid.SelectedItem == null)
            {
                MessageBox.Show("Выборите сотрудника");
                return;
            }
            if (MessageBox.Show("Вы уверены?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                APICommunication.DeleteEmployee(employeesGrid.SelectedItem as Employee, out var response);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Успешно удалено");
                    GetEmployeesResponse();
                }
                else
                {
                    MessageBox.Show(response.ReasonPhrase);
                }
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            GetEmployeesResponse(tbSearch.Text);
        }

        private void btnAll_Click(object sender, RoutedEventArgs e)
        {
            GetEmployeesResponse();
        }
    }
}
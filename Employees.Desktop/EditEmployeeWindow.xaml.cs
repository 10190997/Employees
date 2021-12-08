using Employees.Desktop.Models;
using System.Globalization;
using System.Text;
using System.Windows;

namespace Employees.Desktop
{
    /// <summary>
    /// Interaction logic for EditEmployeeWindow.xaml
    /// </summary>
    public partial class EditEmployeeWindow : Window
    {
        private Employee employee;

        public EditEmployeeWindow(Employee employee)
        {
            InitializeComponent();
            this.employee = employee;
            cbDepartment.ItemsSource = APICommunication.GetDepartments(out _);
            cbDepartment.SelectedIndex = employee.DepartmentId - 1; // костыль
            cbPosition.ItemsSource = APICommunication.GetPositions(out _);
            cbPosition.SelectedIndex = employee.PositionId - 1; // костыль
            DataContext = employee;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
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

            employee.LastName = tbLastName.Text;
            employee.FirstName = tbName.Text;
            employee.Patronymic = tbPatronymic.Text;
            employee.Phone = tbPhone.Text;
            employee.Salary = salary;
            employee.PositionId = cbPosition.SelectedIndex + 1; // костыль
            employee.DepartmentId = cbDepartment.SelectedIndex + 1; // костыль
            APICommunication.PutEmployee(employee, out var response);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Сотрудник успешно изменен");
                Close();
            }
            else
            {
                MessageBox.Show(response.ReasonPhrase);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
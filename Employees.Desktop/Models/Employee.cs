namespace Employees.Desktop.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public decimal Salary { get; set; }

        public int PositionId { get; set; }
        public int DepartmentId { get; set; }
    }
}
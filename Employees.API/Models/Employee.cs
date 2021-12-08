namespace Employees.API.Models
{
    /// <summary>
    /// Сотрудник
    /// </summary>
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }

        // TODO: проверка является ли номером телефона
        public string Phone { get; set; }

        public decimal Salary { get; set; }

        public int PositionId { get; set; }
        public Position Position { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        /*
            {
                "FirstName" : "test",
                "LastName" : "test",
                "Patronymic" : "test",
                "Phone" : "12345",
                "Salary" : "345",
                "PositionId" : "1",
                "DepartmentId" : "1"
            }
        */
    }
}
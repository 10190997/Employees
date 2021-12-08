using Employees.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Employees.Desktop
{
    internal static class APICommunication
    {
        internal static HttpClient RunClient()
        {
            HttpClient client = new();
            client.BaseAddress = new Uri("https://localhost:5001");

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        internal static IEnumerable<Employee> GetEmployees(out string message, string searchString = "")
        {
            IEnumerable<Employee> employees = null;

            HttpClient client = RunClient();

            HttpResponseMessage response = client.GetAsync("api/Employees/" + searchString).Result;

            if (response.IsSuccessStatusCode)
            {
                employees = response.Content.ReadAsAsync<IEnumerable<Employee>>().Result;
            }
            message = response.ReasonPhrase;
            return employees;
        }

        internal static IEnumerable<Position> GetPositions(out string message)
        {
            IEnumerable<Position> positions = null;

            HttpClient client = RunClient();

            HttpResponseMessage response = client.GetAsync("api/Positions").Result;

            if (response.IsSuccessStatusCode)
            {
                positions = response.Content.ReadAsAsync<IEnumerable<Position>>().Result;
            }
            message = response.ReasonPhrase;
            return positions;
        }

        internal static IEnumerable<Department> GetDepartments(out string message)
        {
            IEnumerable<Department> departments = null;
            HttpClient client = RunClient();

            var response = client.GetAsync("api/Departments").Result;

            if (response.IsSuccessStatusCode)
            {
                departments = response.Content.ReadAsAsync<IEnumerable<Department>>().Result;
            }
            message = response.ReasonPhrase;
            return departments;
        }

        internal static void PostEmployee(Employee employee, out HttpResponseMessage response)
        {
            HttpClient client = RunClient();
            response = client.PostAsJsonAsync("api/Employees/", employee).Result;
        }

        internal static void DeleteEmployee(Employee employee, out HttpResponseMessage response)
        {
            HttpClient client = RunClient();
            response = client.DeleteAsync("api/Employees/" + employee.Id).Result;
        }

        internal static void PutEmployee(Employee employee, out HttpResponseMessage response)
        {
            HttpClient client = RunClient();
            response = client.PutAsJsonAsync("api/Employees/", employee).Result;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Department { get; set; }
    public string JobTitle { get; set; }
    public decimal MonthlySalary { get; set; }
    public double TaxRate { get; set; }
    public DateTime HireDate { get; set; }
    public bool IsActive { get; set; }
    public string Email { get; set; }
}

class Program
{
    static List<Employee> employees = new List<Employee>();
    static int nextId = 1001;

    static void Main(string[] args)
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("=== EMPLOYEE PAYROLL & HR MANAGEMENT SYSTEM ===");
            Console.WriteLine("1. Create (Add Employee)");
            Console.WriteLine("2. Read (View All Employees)");
            Console.WriteLine("3. Search / Filter Employees");
            Console.WriteLine("4. Update Employee");
            Console.WriteLine("5. Delete Employee");
            Console.WriteLine("6. Exit");
            Console.Write("\nSelect an option: ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    CreateEmployee();
                    break;
                case "2":
                    ReadEmployees();
                    break;
                case "3":
                    SearchEmployees();
                    break;
                case "4":
                    UpdateEmployee();
                    break;
                case "5":
                    DeleteEmployee();
                    break;
                case "6":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

            if (running)
            {
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }
    }

    static void CreateEmployee()
    {
        Console.WriteLine("--- Add New Employee ---");

        Console.Write("Enter First Name: ");
        string firstName = Console.ReadLine() ?? "";

        Console.Write("Enter Last Name: ");
        string lastName = Console.ReadLine() ?? "";

        Console.Write("Enter Department (e.g., IT, HR, Sales, Finance): ");
        string department = Console.ReadLine() ?? "";

        Console.Write("Enter Job Title: ");
        string jobTitle = Console.ReadLine() ?? "";

        Console.Write("Enter Monthly Salary: ");
        decimal.TryParse(Console.ReadLine(), out decimal salary);

        Console.Write("Enter Tax Rate (e.g., 0.12 for 12%): ");
        double.TryParse(Console.ReadLine(), out double taxRate);

        Console.Write("Enter Hire Date (YYYY-MM-DD): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime hireDate))
        {
            hireDate = DateTime.Today;
        }

        Console.Write("Is Active? (true/false): ");
        string activeInput = (Console.ReadLine() ?? "").Trim().ToLower();
        bool isActive = activeInput == "true" || activeInput == "y" || activeInput == "yes";

        Console.Write("Enter Company Email: ");
        string email = Console.ReadLine() ?? "";

        Employee newEmployee = new Employee
        {
            Id = nextId++,
            FirstName = firstName,
            LastName = lastName,
            Department = department,
            JobTitle = jobTitle,
            MonthlySalary = salary,
            TaxRate = taxRate,
            HireDate = hireDate,
            IsActive = isActive,
            Email = email
        };

        employees.Add(newEmployee);
        Console.WriteLine($"\nEmployee added successfully with ID {newEmployee.Id}!");
    }

    static void DisplayTable(List<Employee> list)
    {
        if (!list.Any())
        {
            Console.WriteLine("No employee records found.");
            return;
        }

        Console.WriteLine($"{"ID",-5} | {"First Name",-11} | {"Last Name",-11} | {"Dept",-9} | {"Job Title",-17} | {"Salary",-10} | {"Tax",-6} | {"Hire Date",-10} | {"Active",-6} | {"Email"}");
        Console.WriteLine(new string('-', 128));

        foreach (var emp in list)
        {
            Console.WriteLine($"{emp.Id,-5} | {emp.FirstName,-11} | {emp.LastName,-11} | {emp.Department,-9} | {emp.JobTitle,-17} | ${emp.MonthlySalary,-9:F2} | {emp.TaxRate,-6:F2} | {emp.HireDate:yyyy-MM-dd} | {emp.IsActive,-6} | {emp.Email}");
        }
    }

    static void ReadEmployees()
    {
        Console.WriteLine("--- Employee Payroll & Directory ---");
        var sortedEmployees = employees.OrderBy(e => e.Id).ToList();
        DisplayTable(sortedEmployees);
    }

    static void SearchEmployees()
    {
        Console.WriteLine("--- Search / Filter Employees ---");
        Console.Write("Enter keyword (Name, Department, Job Title, or Email): ");
        string keyword = Console.ReadLine() ?? "";

        var results = employees.Where(e =>
            e.FirstName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            e.LastName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            e.Department.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            e.JobTitle.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            e.Email.Contains(keyword, StringComparison.OrdinalIgnoreCase)
        ).ToList();

        Console.WriteLine();
        DisplayTable(results);
    }

    static void UpdateEmployee()
    {
        ReadEmployees();
        if (!employees.Any()) return;

        Console.Write("\nEnter the ID of the employee to update: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Employee emp = employees.FirstOrDefault(e => e.Id == id);

            if (emp != null)
            {
                Console.Write($"Enter new First Name (current: {emp.FirstName}): ");
                string firstName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(firstName)) emp.FirstName = firstName;

                Console.Write($"Enter new Last Name (current: {emp.LastName}): ");
                string lastName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(lastName)) emp.LastName = lastName;

                Console.Write($"Enter new Department (current: {emp.Department}): ");
                string dept = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(dept)) emp.Department = dept;

                Console.Write($"Enter new Job Title (current: {emp.JobTitle}): ");
                string title = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(title)) emp.JobTitle = title;

                Console.Write($"Enter new Monthly Salary (current: {emp.MonthlySalary}): ");
                if (decimal.TryParse(Console.ReadLine(), out decimal salary)) emp.MonthlySalary = salary;

                Console.Write($"Enter new Tax Rate (current: {emp.TaxRate}): ");
                if (double.TryParse(Console.ReadLine(), out double tax)) emp.TaxRate = tax;

                Console.Write($"Enter new Hire Date YYYY-MM-DD (current: {emp.HireDate:yyyy-MM-dd}): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime hireDate)) emp.HireDate = hireDate;

                Console.Write($"Enter new Active status true/false (current: {emp.IsActive}): ");
                string activeStr = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(activeStr))
                {
                    string clean = activeStr.Trim().ToLower();
                    emp.IsActive = clean == "true" || clean == "y" || clean == "yes";
                }

                Console.Write($"Enter new Email (current: {emp.Email}): ");
                string email = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(email)) emp.Email = email;

                Console.WriteLine("\nEmployee updated successfully!");
            }
            else
            {
                Console.WriteLine("\nEmployee not found.");
            }
        }
        else
        {
            Console.WriteLine("\nInvalid ID format.");
        }
    }

    static void DeleteEmployee()
    {
        ReadEmployees();
        if (!employees.Any()) return;

        Console.Write("\nEnter the ID of the employee to delete: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Employee emp = employees.FirstOrDefault(e => e.Id == id);
            if (emp != null)
            {
                employees.Remove(emp);
                Console.WriteLine("\nEmployee deleted successfully!");
            }
            else
            {
                Console.WriteLine("\nEmployee not found.");
            }
        }
        else
        {
            Console.WriteLine("\nInvalid ID format.");
        }
    }
}
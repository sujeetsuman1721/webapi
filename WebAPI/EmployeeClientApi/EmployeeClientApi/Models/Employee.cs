using System;

namespace EmployeeClientApi.Models
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Salary { get; set; }
        public bool? Permanet { get; set; }
        public string Deparrment { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}

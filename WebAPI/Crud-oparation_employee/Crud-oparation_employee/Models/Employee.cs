using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Crud_oparation_employee.Models
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

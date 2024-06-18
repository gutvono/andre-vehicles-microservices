using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Employee : Person
    {
        public decimal ComissionValue { get; set; }
        public decimal Comission { get; set; }

        public Employee() { }

        public Employee(EmployeeDTO employeeDTO)
        {
            Document = employeeDTO.Document;
            Name = employeeDTO.Name;
            BirthDate = employeeDTO.BirthDate;
            Address = new Address
            {
                PostalCode = employeeDTO.AddressDTO.PostalCode,
                Number = employeeDTO.AddressDTO.Number,
                Complement = employeeDTO.AddressDTO.Complement,
                Street = string.Empty,
                StreetType = string.Empty,
                City = string.Empty,
                State = string.Empty,
                Neighborhood = string.Empty
            };
            Email = employeeDTO.Email;
            ComissionValue = employeeDTO.ComissionValue;
            Comission = employeeDTO.Comission;
        }
    }
}

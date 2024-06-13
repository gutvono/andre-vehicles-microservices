using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class EmployeeDTO : PersonDTO
    {
        RoleDTO RoleDTO { get; set; }
        public decimal ComissionValue { get; set; }
        public decimal Comission { get; set; }
    }
}

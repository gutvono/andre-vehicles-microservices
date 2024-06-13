using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PersonDTO
    {
        [Key] public string Document { get; set; }
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public AddressDTO AddressDTO { get; set; }
        public string Email { get; set; }
    }
}

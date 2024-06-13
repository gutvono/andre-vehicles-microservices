using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CustomerDTO : Person
    {
        public string PdfDocument { get; set; }
        public decimal Income { get; set; }
    }
}

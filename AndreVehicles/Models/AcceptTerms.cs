using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class AcceptTerms
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public Term Term { get; set; }
        public DateTime AcceptDate { get; set; }
    }
}

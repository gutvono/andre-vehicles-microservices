using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Term
    {
        public int Id {  get; set; }
        public string Text { get; set; }
        public string Version { get; set; }
        public DateTime RegistryDate { get; set; }
        public bool Status { get; set; }
    }
}

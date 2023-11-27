using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public record address
    {

       
        public string StreetName { get; set; }
        public string Community { get; set; }    
        public string parish { get; set; }

        public address(string streetName, string community, string parish)
        {
            this.StreetName = streetName;
            this.Community = community;
            this.parish = parish;
        }

        public address() { }
    }
}

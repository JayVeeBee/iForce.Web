using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iForce.Domain
{
    public class CustomerFilter
    {
        public DateTime? MinDateOfBirth { get; set; }
        public DateTime? MaxDateOfBirth { get; set; }
        public bool IncludeVehicles { get; set; }
    }
}

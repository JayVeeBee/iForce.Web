using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iForce.Domain
{
    public class VehicleFilter
    {
        public DateTime? MaxRegistrationDate { get; set; }
        public int? MinEngineSizeCc { get; set; }
    }
}

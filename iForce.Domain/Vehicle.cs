using System.ComponentModel.DataAnnotations;

namespace iForce.Domain
{
    public class Vehicle : VehicleBase
    {
        [Key]
        public int VehicleId { get; set; }
    }
}
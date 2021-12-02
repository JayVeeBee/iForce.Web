using System.ComponentModel.DataAnnotations;

namespace iForce.Domain
{
    public class Customer : CustomerBase
    {
        [Key]
        public int CustomerId { get; set; }
        public List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
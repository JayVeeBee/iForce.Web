using System.ComponentModel.DataAnnotations;

namespace iForce.Domain
{
    public class Customer : CustomerBase
    {
        [Key]
        public int CustomerId { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
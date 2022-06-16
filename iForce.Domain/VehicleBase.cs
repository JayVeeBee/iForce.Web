using System.ComponentModel.DataAnnotations;

namespace iForce.Domain
{
    public class VehicleBase
    {
        [MaxLength(50)]
        public string Manufacturer { get; set; } = "";
        [MaxLength(50)]
        public string Model { get; set; } = "";
        [MaxLength(20)]
        public string RegistrationNumber { get; set; } = "";
        public DateTime RegistrationDate { get; set; }
        public int EngineSizeCc { get; set; }
        [MaxLength(50)]
        public string InteriorColour { get; set; } = "";
        public int CustomerId { get; set; }

        public void Update(VehicleBase source)
        {
            if (source != null)
            {
                Manufacturer = source.Manufacturer;
                Model = source.Model;
                RegistrationNumber = source.RegistrationNumber;
                RegistrationDate = source.RegistrationDate;
                EngineSizeCc = source.EngineSizeCc;
                InteriorColour = source.InteriorColour;
                CustomerId = source.CustomerId;
            }
        }

        public virtual void SetDatesLocalIfUnspecified()
        {
            RegistrationDate = RegistrationDate.SetLocalIfUnspecified();
        }
    }
}
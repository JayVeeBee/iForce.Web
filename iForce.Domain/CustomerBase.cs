using System.ComponentModel.DataAnnotations;

namespace iForce.Domain
{
    public class CustomerBase
    {
        [MaxLength(50)]
        public string Forename { get; set; } = "";
        [MaxLength(50)]
        public string Surname { get; set; } = "";
        public DateTime DateOfBirth { get; set; }

        public void Update(CustomerBase source)
        {
            if (source != null)
            {
                Forename = source.Forename;
                Surname = source.Surname;
                DateOfBirth = source.DateOfBirth;
            }
        }

        public void SetDatesLocalIfUnspecified()
        {
            DateOfBirth = DateOfBirth.SetLocalIfUnspecified();
        }
    }
}
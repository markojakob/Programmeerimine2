using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KooliProjekt.Data
{
    public class Customer : Entity
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int PhoneNum {  get; set; }
        [Required]
        public string Address { get; set; }

        [NotMapped]
        public string FullName 
        { 
            get { return FirstName + " " + LastName; }
        }
        public IList<Car> Cars { get; set; }

        public Customer()
        {
            Cars = new List<Car>();
        }
    }
}

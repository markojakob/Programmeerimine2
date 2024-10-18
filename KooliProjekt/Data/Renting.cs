using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class Renting
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(25)]
        public string RentalNo { get; set; }
        [Required] 
        public DateTime? RentalDate { get; set; }
        [Required]
        public DateTime? RentalDueTime { get; set; }

        public decimal DriveDistance { get; set; }

        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public IList<Car> Lines { get; set; }

        public Renting()
        {
            Lines = new List<Car>();
        }

      

    }
}

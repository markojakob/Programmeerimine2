using System;
using System.ComponentModel.DataAnnotations;

namespace RentingApi
{
    public class Renting
    {
        public int Id { get; set; }
        [Required]
        public int RentalNo { get; set; }
        [Required] 
        public DateTime? RentalDate { get; set; }
        [Required]
        public DateTime? RentalDueTime { get; set; }

        public decimal DriveDistance { get; set; }

        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public Car Car { get; set; }
        public int CarId { get; set; }
    }
}
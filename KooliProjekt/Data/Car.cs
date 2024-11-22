using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class Car : Entity
    {
        [Required]
        [StringLength(30)]
        public string Model  { get; set; }
        [Required]
        public string CarMaker { get; set; }

        [Required]
        public decimal Price { get; set; }
        public string Colour { get; set; }
        public string Description { get; set; }

        [Required]
        public decimal KmTariff { get; set; }
        [Required]
        public string Category { get; set; }

        public IList<Renting> Rentings { get; set; }

        public IList<Customer> Cars { get; set; }

    }
}

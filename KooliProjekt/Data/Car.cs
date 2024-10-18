using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class Car
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Model  { get; set; }
        [Required]
        public string CarMaker { get; set; }

        [Required]
        [StringLength(30)]
        public decimal Price { get; set; }
        public string Colour { get; set; }
        public string Description { get; set; }

        [Required]
        [StringLength(20)]
        public decimal KmTariff { get; set; }
        [Required]
        public string Category { get; set; }

        
    }
}

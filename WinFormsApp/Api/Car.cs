using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Api
{
    class Car
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Model { get; set; }
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
    }
}

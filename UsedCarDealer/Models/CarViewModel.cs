using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using UsedCarDealer.Interfaces;

namespace UsedCarDealer.Models
{
    public class CarViewModel : ICar
    {   
        [Key]
        public int Id { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        [Range(1886, int.MaxValue, ErrorMessage = "Price Should be greated than or equal 1886")]

        public int YearProduction { get; set; }
        [Range(1,int.MaxValue,ErrorMessage ="Price Should be greated than or equal 1")]
        public decimal Price { get; set; }
    }
}

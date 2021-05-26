using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using UsedCarDealer.Interfaces;

namespace UsedCarDealer.Entities
{
    public class Car : ICar
    {
        public int Id { get; set ; }
        public string Brand { get; set; }
        public string Model { get ; set ; }
        public int YearProduction { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set ; }
    }
}

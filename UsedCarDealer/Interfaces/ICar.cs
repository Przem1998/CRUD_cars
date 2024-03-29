﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsedCarDealer.Interfaces
{
    interface ICar
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int YearProduction { get; set; }
        public decimal Price { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsedCarDealer.Entities;

namespace UsedCarDealer
{
    public class CarServices
    {
        public List<Car> Cars { get; private set; } = new List<Car>();
        public CarServices() { }

       

    }
}

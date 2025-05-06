using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavtorShiper.Entities
{
    public class PassengerShip : Ship
    {
        public Dictionary<int, Passenger> Passengers { get; } = new();

        public PassengerShip(string imoNumber, string name, double length, double width, Dictionary<int, Passenger>? passengers = null)
            : base(imoNumber, name, length, width) { }
    }
}

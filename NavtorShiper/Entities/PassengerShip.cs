using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavtorShiper.Entities
{
    public class PassengerShip : Ship
    {
        public Dictionary<int, Passenger> Passengers { get; }

        public PassengerShip(string imoNumber, string name, double length, double width, Dictionary<int, Passenger>? passengers = null)
            : base(imoNumber, name, length, width)
        {
            Passengers = passengers ?? new();
        }

        public override string ToString()
        {
            var strBuilder = new StringBuilder();
            strBuilder.Append(base.ToString());
            var passengers = Passengers.Values;
            foreach (var passenger in passengers)
            {
                strBuilder
                    .Append('\t')
                    .Append(passenger.ToString())
                    .Append('\n');
            }
            return strBuilder.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavtorShiper.Entities
{
    public class PassengerShip : Ship
    {
        public List<Passenger> Passengers { get; }

        public PassengerShip(string imoNumber, string name, double length, double width, List<Passenger>? passengers = null)
            : base(imoNumber, ShipType.Passenger, name, length, width)
        {
            Passengers = passengers ?? new();
        }

        public override string ToString()
        {
            var strBuilder = new StringBuilder();
            strBuilder.Append(base.ToString());
            foreach (var passenger in Passengers)
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

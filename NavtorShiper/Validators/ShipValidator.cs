using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavtorShiper.Entities;

namespace NavtorShiper.Validators
{
    public class ShipValidator
    {
        public static void ValidateShipParameters(Ship ship)
        {
            if (ship.Length <= 0 || ship.Width <= 0)
            {
                throw new ArgumentException("Length and width must be positive numbers.");
            }
        }

        public static void ValidateShipType<TShipType>(Ship ship)
        {
            if (ship is not TShipType)
            {
                throw new ArgumentException($"Ship with imo {ship.ImoNumber} is wrong ship type.");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavtorShiper.Utils;

namespace NavtorShiper.Entities
{
    public abstract class Ship
    {
        public string ImoNumber { get; protected set; }
        public string Name { get; set; }
        public double Length { get; protected set; }
        public double Width { get; protected set; }

        protected Ship(string imoNumber, string name, double length, double width)
        {
            ValidateShipParameters(imoNumber, length, width);
            ImoNumber = imoNumber;
            Name = name;
            Length = length;
            Width = width;
        }

        private static void ValidateShipParameters(string imoNumber, double length, double width)
        {
            if (!ImoValidator.IsValidImoNumber(imoNumber))
            {
                throw new ArgumentException($"Invalid IMO number: {imoNumber}");
            }

            if (length < 0 || width < 0)
            {
                throw new ArgumentException("Length and width must be positive numbers.");
            }
        }

        public override string ToString()
        {
            return $"SHIP - ImoNumber: {ImoNumber}, Name: {Name}, Length: {Length}m, Width: {Width}m\n";
        }
    }
}

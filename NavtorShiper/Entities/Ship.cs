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
        protected Ship(string imoNumber, string name, int length, int width)
        {
            if (!ImoValidator.IsValidImoNumber(imoNumber))
            {
                throw new ArgumentException($"Invalid IMO number: {imoNumber}");
            }
            if (length < 0 || width < 0)
            {
                throw new ArgumentException($"Length and width must be positive numbers.");
            }
            ImoNumber = imoNumber;
            Name = name;
            Length = length;
            Width = width;
        }

        public string ImoNumber { get; protected set; }
        public string Name { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }

        public override string ToString()
        {
            return $"SHIP - ImoNumber: {ImoNumber}, Name: {Name}, Length: {Length}m, Width: {Width}m\n";
        }
    }
}

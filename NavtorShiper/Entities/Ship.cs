using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using NavtorShiper.Utils;

namespace NavtorShiper.Entities
{
    public enum ShipType
    {
        Passenger,
        Tanker
    }

    public abstract class Ship
    {
        public string ImoNumber { get; }
        public string Name { get; }
        public double Length { get; }
        public double Width { get; }
        public ShipType Type { get; protected set; }

        protected Ship(string imoNumber, ShipType type, string name, double length, double width)
        {
            ImoNumber = imoNumber;
            Type = type;
            Name = name;
            Length = length;
            Width = width;
        }

        public override string ToString()
        {
            return $"SHIP - ImoNumber: {ImoNumber}, Name: {Name}, Length: {Length}m, Width: {Width}m\n";
        }
    }
}

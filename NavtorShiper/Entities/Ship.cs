using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavtorShiper.Entities
{
    public abstract class Ship(string imoNumber, string name, int length, int width)
    {
        public string IMONumber { get; protected set; } = imoNumber;
        public string Name { get; set; } = name;
        public int Length { get; set; } = length;
        public int Width { get; set; } = width;

        public override string ToString()
        {
            return $"SHIP - IMONumber: {IMONumber}, Name: {Name}, Length: {Length}m, Width: {Width}m\n";
        }
    }
}

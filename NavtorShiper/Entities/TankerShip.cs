using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavtorShiper.Entities
{
    public class TankerShip(string imoNumber, string name, int length, int width) : Ship(imoNumber, name, length, width)
    {
        public Dictionary<int, Tank> Tanks { get; set; } = new();
    }
}

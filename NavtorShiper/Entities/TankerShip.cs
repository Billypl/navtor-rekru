using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavtorShiper.Entities
{
    public class TankerShip(string imoNumber, string name, int length, int width, Dictionary<int, Tank>? tanks = null) : Ship(imoNumber, name, length, width)
    {
        public Dictionary<int, Tank> Tanks { get; set; } = tanks ?? new();
        public override string ToString()
        {
            var tanks = Tanks.Values;
            var strBuilder = new StringBuilder();
            strBuilder.Append(base.ToString());
            foreach (var tank in tanks)
            {
                strBuilder.Append(tank.ToString());
            }
            return strBuilder.ToString();
        }
    }
}

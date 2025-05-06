using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavtorShiper.Entities
{
    public class TankerShip : Ship
    {
        public TankerShip(string imoNumber, string name, double length, double width, Dictionary<int, ITank>? tanks = null) : base(imoNumber, name, length, width)
        {
            Tanks = tanks ?? new();
        }

        public Dictionary<int, ITank> Tanks { get; set; }
        public override string ToString()
        {
            var tanks = Tanks.Values;
            var strBuilder = new StringBuilder();
            strBuilder.Append(base.ToString());
            foreach (var tank in tanks)
            {
                strBuilder.Append('\t').Append(tank.ToString()).Append('\n');
            }
            return strBuilder.ToString();
        }
    }
}

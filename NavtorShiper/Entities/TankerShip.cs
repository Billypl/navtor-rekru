using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavtorShiper.Entities
{
    public class TankerShip : Ship
    {
        public List<Tank> Tanks { get; set; }

        public TankerShip(string imoNumber, string name, double length, double width, List<Tank>? tanks = null) : base(imoNumber, name, length, width)
        {
            Tanks = tanks ?? new();
        }

        public override string ToString()
        {
            var strBuilder = new StringBuilder();
            strBuilder.Append(base.ToString());
            foreach (var tank in Tanks)
            {
                strBuilder
                    .Append('\t')
                    .Append(tank.ToString())
                    .Append('\n');
            }
            return strBuilder.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NavtorShiper.Entities;

namespace NavtorShiper.Repositories
{
    public class ShipRepository : IShipRepository
    {
        private readonly List<Ship> _ships = new();

        public IEnumerable<Ship> GetAll() => _ships;

        public Ship? GetById(string imo) => _ships.FirstOrDefault(s => s.ImoNumber == imo);

        public void Add(Ship ship) => _ships.Add(ship);

        public bool Delete(Ship ship) => _ships.Remove(ship);
    }
}

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
        private readonly Dictionary<string, Ship> _ships = new();

        public IEnumerable<Ship> GetAll()
        {
            return _ships.Values;
        }

        public Ship? GetById(string imo)
        {
            var ship = _ships.GetValueOrDefault(imo);
            return ship;
        }

        public void Add(Ship ship)
        {
            if (_ships.ContainsKey(ship.ImoNumber))
            {
                throw new ArgumentException($"Ship with IMO {ship.ImoNumber} already exists.");
            }
            _ships.Add(ship.ImoNumber, ship);
        }

        public bool Delete(string id)
        {
            return _ships.Remove(id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavtorShiper.Entities;
using NavtorShiper.Repositories;

namespace NavtorShiper.Data
{
    class Seeder(IShipRepository _shipRepository)
    {
        public void FillShipRepository()
        {
            var ship1 = new TankerShip("1234567", "Tanker 1", 1000, 200, new Dictionary<int, Tank>
            {
                { 1, new Tank(1, FuelType.Diesel, 1000, 0) },
                { 2, new Tank(2, FuelType.HeavyFuel, 500, 0) }
            });
            var ship2 = new TankerShip("7654321", "Tanker 2", 2000, 400, new Dictionary<int, Tank>
            {
                { 3, new Tank(3, FuelType.Diesel, 700, 0) },
                { 4, new Tank(4, FuelType.HeavyFuel, 400, 0) }
            });
            _shipRepository.Add(ship1);
            _shipRepository.Add(ship2);
        }
    }
}

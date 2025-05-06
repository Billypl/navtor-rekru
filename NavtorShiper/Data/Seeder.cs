using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavtorShiper.Entities;
using NavtorShiper.Repositories;

namespace NavtorShiper.Data
{
    class Seeder(IShipRepository _shipRepository)
    {
        public const string ValidImo1 = "8814275";
        public const string ValidImo2 = "9074729";
        public const string ValidImo3 = "1234567";
        public void FillShipRepository()
        {
            var ship1 = new TankerShip(ValidImo1, "Tanker Ship 1", 1000, 200, new Dictionary<int, ITank>
            {
                { 1, new Tank(1, 1000) },
                { 2, new Tank(2, 500) }
            });
            var ship2 = new TankerShip(ValidImo2, "Tanker Ship 2", 2000, 400, new Dictionary<int, ITank>
            {
                { 1, new Tank(1, 700) },
                { 2, new Tank(2, 400) }
            });
            var ship3 = new PassengerShip(ValidImo3, "Passenger Ship 3", 500, 400, new Dictionary<int, Passenger>
            {
                { 1, new Passenger(1, "John", "Doe") },
                { 2, new Passenger(2, "Jane",  "Smith") },
            });
            _shipRepository.Add(ship1);
            _shipRepository.Add(ship2);
            _shipRepository.Add(ship3);
        }
    }
}

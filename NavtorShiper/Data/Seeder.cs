using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NavtorShiper.Entities;
using NavtorShiper.Repositories;

namespace NavtorShiper.Data
{
    public class Seeder
    {
        private readonly IShipRepository _shipRepository;

        public Seeder(IShipRepository shipRepository)
        {
            _shipRepository = shipRepository;
        }

        public const string ValidImo1 = "8814275";
        public const string ValidImo2 = "9074729";
        public const string ValidImo3 = "1234567";
        private const string jsonFilePath = "Data\\data.json";
        public void FillShipRepository()
        {
            var ship1 = new TankerShip(ValidImo1,  "Tanker Ship 1", 1000, 200, new List<Tank>
            {
                { new Tank(1, 1000) },
                { new Tank(2, 500) }
            });
            //var ship2 = new TankerShip(ValidImo2,  "Tanker Ship 2", 2000, 400, new List<Tank>
            //{
            //    { new Tank(1, 700) },
            //    { new Tank(2, 400) }
            //});
            var ship3 = new PassengerShip(ValidImo3, "Passenger Ship 3", 500, 400, new List<Passenger>
            {
                { new Passenger(1, "John", "Doe") },
                { new Passenger(2, "Jane", "Smith") },
            });
            _shipRepository.Add(ship1);
            //_shipRepository.Add(ship2);
            _shipRepository.Add(ship3);
        }
        //public void FillShipRepository(string filePath)
        //{
        //    string projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\.."));
        //    string path = Path.Combine(projectRoot, jsonFilePath);
        //    //string path = Path.Combine(AppContext.BaseDirectory, jsonFilePath);
        //    string jsonString = File.ReadAllText("");
        //    List <Ship> shipCollection = JsonSerializer.Deserialize<List<Ship>>(jsonString)!;

        //    foreach (var shipData in shipCollection)
        //    {
        //        if (shipData.Type == ShipType.Tanker)
        //        {
        //            _shipRepository.Add(shipData as TankerShip);
        //        }
        //        else if (shipData.Type == ShipType.Tanker)
        //        {
        //            _shipRepository.Add(shipData as PassengerShip);
        //        }
        //    }
        //}
    }
}

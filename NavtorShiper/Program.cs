using NavtorShiper.Data;
using NavtorShiper.Entities;
using NavtorShiper.Repositories;
using NavtorShiper.Services;
using NavtorShiper.Utils;

IShipRepository shipRepository = new ShipRepository();
Seeder seeder = new Seeder(shipRepository);
seeder.FillShipRepository();

var tankerShipService = new TankerShipService(shipRepository);
var passengerShipService = new PassengerShipService(shipRepository);

Console.WriteLine("------ TANKER 1 ------");
shipRepository.GetAll().Where(s => s is TankerShip).ToList().ForEach(Console.WriteLine);
tankerShipService.RefuelTank(Seeder.ValidImo1, 1, FuelType.HeavyFuel, 200);
tankerShipService.RefuelTank(Seeder.ValidImo2, 2, FuelType.Diesel, 300);
Console.WriteLine("------ TANKER 2 ------");
shipRepository.GetAll().Where(s => s is TankerShip).ToList().ForEach(Console.WriteLine);
tankerShipService.EmptyTank(Seeder.ValidImo2, 2);
Console.WriteLine("------ TANKER 3 ------");
shipRepository.GetAll().Where(s => s is TankerShip).ToList().ForEach(Console.WriteLine);

Console.WriteLine("------ --------- ------\n");

Console.WriteLine("------ PASSENGER 1 ------");
shipRepository.GetAll().Where(s => s is PassengerShip).ToList().ForEach(Console.WriteLine);
Console.WriteLine("------ PASSENGER 2 ------");
passengerShipService.AddPassenger(Seeder.ValidImo3, new Passenger(3, "Megan", "Fox"));
shipRepository.GetAll().Where(s => s is PassengerShip).ToList().ForEach(Console.WriteLine);
Console.WriteLine("------ PASSENGER 3 ------");
passengerShipService.RemovePassenger(Seeder.ValidImo3, 1);
shipRepository.GetAll().Where(s => s is PassengerShip).ToList().ForEach(Console.WriteLine);

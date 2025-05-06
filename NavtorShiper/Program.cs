using NavtorShiper.Data;
using NavtorShiper.Entities;
using NavtorShiper.Repositories;
using NavtorShiper.Services;

IShipRepository shipRepository = new ShipRepository();
Seeder seeder = new Seeder(shipRepository);
seeder.FillShipRepository();

TankerShipService tankerShipService = new TankerShipService(shipRepository);

tankerShipService.RefuelTank("1234567", 1, FuelType.Diesel, 300);
tankerShipService.RefuelTank("7654321", 3, FuelType.HeavyFuel, 200);
tankerShipService.EmptyTank("7654321", 3);
Console.WriteLine(shipRepository.GetById("1234567")?.ToString());

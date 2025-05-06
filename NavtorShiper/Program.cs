using System.Text;
using DiffPlex.DiffBuilder;
using DiffPlex;
using NavtorShiper.Data;
using NavtorShiper.Entities;
using NavtorShiper.Repositories;
using NavtorShiper.Services;
using NavtorShiper.Utils;
using DiffPlex.DiffBuilder.Model;

IShipRepository shipRepository = new ShipRepository();
Seeder seeder = new Seeder(shipRepository);
seeder.FillShipRepository();

var tankerShipService = new TankerShipService(shipRepository);
var passengerShipService = new PassengerShipService(shipRepository);

PrintTankerOperations();
Console.WriteLine("------ --------- ------\n");
PrintPassengerOperations();

void PrintTankerOperations()
{
    Console.WriteLine("------ TANKER 1 ------");
    var out1 = string.Join('\n', shipRepository.GetAll().OfType<TankerShip>().ToList());
    Console.WriteLine(out1);
    
    Console.WriteLine("------ TANKER 2 ------");
    tankerShipService.RefuelTank(Seeder.ValidImo1, 1, FuelType.HeavyFuel, 200);
    tankerShipService.RefuelTank(Seeder.ValidImo2, 2, FuelType.Diesel, 300);
    string out2 = ShowDiffComparedToCurrent<TankerShip>(shipRepository, out1);

    Console.WriteLine("------ TANKER 3 ------");
    tankerShipService.EmptyTank(Seeder.ValidImo2, 2);
    string out3 = ShowDiffComparedToCurrent<TankerShip>(shipRepository, out2);
}

void PrintPassengerOperations()
{
    Console.WriteLine("------ PASSENGER 1 ------");
    var out1 = string.Join('\n', shipRepository.GetAll().OfType<PassengerShip>().ToList());
    Console.WriteLine(out1);

    Console.WriteLine("------ PASSENGER 2 ------");
    passengerShipService.AddPassenger(Seeder.ValidImo3, new Passenger(3, "Megan", "Fox"));
    string out2 = ShowDiffComparedToCurrent<PassengerShip>(shipRepository, out1);

    Console.WriteLine("------ PASSENGER 3 ------");
    passengerShipService.RemovePassenger(Seeder.ValidImo3, 1);
    string out3 = ShowDiffComparedToCurrent<PassengerShip>(shipRepository, out2);
}

string ShowDiffComparedToCurrent<T>(IShipRepository shipRepository1, string lastState)
{
    var newState = string.Join('\n', shipRepository1.GetAll().OfType<T>().ToList());
    DiffViewr.ShowDiff(lastState, newState);
    return newState;
}
using NavtorShiper.Data;
using NavtorShiper.Entities;
using NavtorShiper.Repositories;
using NavtorShiper.Services;
using NavtorShiper.Utils;
using NavtorShiper.Validators;

// this is program demo - for full functionality showcase, please refer to the tests project

IShipRepository shipRepository = new ShipRepository();
Seeder seeder = new Seeder(shipRepository);
seeder.FillShipRepository();

var shipService = new ShipService(shipRepository);
var tankerShipService = new TankerShipService(shipService);
var passengerShipService = new PassengerShipService(shipService);

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
    string out2 = ShowDiffComparedToCurrent<TankerShip>(shipService, out1);

    Console.WriteLine("------ TANKER 3 ------");
    tankerShipService.EmptyTank(Seeder.ValidImo2, 2);
    string out3 = ShowDiffComparedToCurrent<TankerShip>(shipService, out2);
}

void PrintPassengerOperations()
{
    Console.WriteLine("------ PASSENGER 1 ------");
    var out1 = string.Join('\n', shipRepository.GetAll().OfType<PassengerShip>().ToList());
    Console.WriteLine(out1);

    Console.WriteLine("------ PASSENGER 2 ------");
    passengerShipService.AddPassenger(Seeder.ValidImo3, new Passenger(3, "Megan", "Fox"));
    string out2 = ShowDiffComparedToCurrent<PassengerShip>(shipService, out1);

    Console.WriteLine("------ PASSENGER 3 ------");
    passengerShipService.RemovePassenger(Seeder.ValidImo3, 1);
    string out3 = ShowDiffComparedToCurrent<PassengerShip>(shipService, out2);
}

string ShowDiffComparedToCurrent<T>(ShipService shipService, string lastState)
{
    var newState = string.Join('\n', shipService.GetAll().OfType<T>().ToList());
    DiffViewer.ShowDiff(lastState, newState);
    return newState;
}
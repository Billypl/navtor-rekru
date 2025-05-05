using NavtorShiper.Entities;
using NavtorShiper.Repositories;
using NavtorShiper.Services;

IShipRepository shipRepository = new ShipRepository();

TankerShipService tankerShipService = new TankerShipService(shipRepository);
tankerShipService.RefuelTank("1234567", 1, FuelType.HeavyFuel, 300);

void FillShipRepository()
{
    var ship1 = new TankerShip("1234567", "Tanker 1", 1000, 200,new Dictionary<int, Tank>
    {
        { 1, new Tank(1, FuelType.Diesel, 1000, 0) },
        { 2, new Tank(2, FuelType.HeavyFuel, 500, 0) }
    });
    var ship2 = new TankerShip("7654321", "Tanker 2", 2000, 400, new Dictionary<int, Tank>
    {
        { 3, new Tank(3, FuelType.Diesel, 700, 0) },
        { 4, new Tank(4, FuelType.HeavyFuel, 400, 0) }
    });
    shipRepository.Add(ship1);
    shipRepository.Add(ship2);
}
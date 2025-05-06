using NavtorShiper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavtorShiper.Repositories;

namespace NavtorShiper.Services
{
    class PassengerShipService
    {
        private readonly IShipRepository _shipRepository;

        public PassengerShipService(IShipRepository shipRepository)
        {
            _shipRepository = shipRepository;
        }

        public void AddPassenger(string imo, Passenger passenger)
        {
            GetPassengerShip(imo).Passengers.Add(passenger);
        }

        public void RemovePassenger(string imo, int passengerId)
        {
            var ship = GetPassengerShip(imo);
            var passenger = ship.Passengers.FirstOrDefault(p => p.Id == passengerId);
            if (passenger is null)
            {
                throw new InvalidOperationException($"Passenger with id {passengerId} not found.");
            }
            ship.Passengers.Remove(passenger);
        }

        private PassengerShip GetPassengerShip(string imo)
        {
            var ship = _shipRepository.GetById(imo);
            if (ship is null)
            {
                throw new ArgumentException($"Ship with IMO {imo} not found.");
            }
            if (ship is not PassengerShip passengerShip)
            {
                throw new InvalidOperationException($"Ship with IMO {imo} is not a passenger ship.");
            }
            return passengerShip;
        }
    }
}

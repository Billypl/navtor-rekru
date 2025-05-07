using NavtorShiper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavtorShiper.Repositories;
using NavtorShiper.Validators;

namespace NavtorShiper.Services
{
    public interface IPassengerShipService
    {
        public void AddPassenger(string imo, Passenger passenger);
        public void RemovePassenger(string imo, int passengerId);
    }

    public class PassengerShipService : IPassengerShipService
    {
        private readonly IShipService _shipService;

        public PassengerShipService(IShipService shipService)
        {
            _shipService = shipService;
        }

        public void AddPassenger(string imo, Passenger passenger)
        {
            var ship = GetPassengerShip(imo);
            if (ship.Passengers.Any(p => p.Id == passenger.Id))
            {
                throw new ArgumentException($"Passenger with id {passenger.Id} already exists.");
            }
            ship.Passengers.Add(passenger);
        }

        public void RemovePassenger(string imo, int passengerId)
        {
            var ship = GetPassengerShip(imo);
            var passenger = GetPassenger(passengerId, ship);
            ship.Passengers.Remove(passenger);
        }

        private PassengerShip GetPassengerShip(string imo)
        {
            var ship = _shipService.GetById(imo);
            ShipValidator.ValidateShipType<PassengerShip>(ship);
            return ship as PassengerShip;
        }

        private Passenger GetPassenger(int passengerId, PassengerShip ship)
        {
            var passenger = ship.Passengers.FirstOrDefault(p => p.Id == passengerId);
            if (passenger is null)
            {
                throw new ArgumentException($"Passenger with id {passengerId} not found.");
            }
            return passenger;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavtorShiper.Entities;
using NavtorShiper.Repositories;

namespace NavtorShiper.Services
{
    public class TankerShipService(IShipRepository _shipRepository)
    {
        public void RefuelTank(string imo, int tankId, FuelType fuelType, double amount)
        {
            Tank tank = GetTank(imo, tankId);
            ValidateTank(tankId, fuelType, amount, tank);
            tank.CurrentLevel += amount;
        }

        public void EmptyTank(string imo, int tankId)
        {
            Tank tank = GetTank(imo, tankId);
            tank.CurrentLevel = 0;
        }

        private Tank GetTank(string imo, int tankId)
        {
            var ship = _shipRepository.GetById(imo);
            if (ship is null)
            {
                throw new ArgumentException($"Ship with IMO {imo} not found.");
            }
            if (ship is not TankerShip tankerShip)
            {
                throw new InvalidOperationException($"Ship with IMO {imo} is not a tanker ship.");
            }
            var tank = tankerShip.Tanks.GetValueOrDefault(tankId);
            if (tank is null)
            {
                throw new ArgumentException($"Tank with ID {tankId} not found in ship with IMO {imo}.");
            }
            return tank;
        }

        private static void ValidateTank(int tankId, FuelType fuelType, double amount, Tank tank)
        {
            if (tank.Type != fuelType)
            {
                throw new InvalidOperationException($"Tank with ID {tankId} does not support fuel type {fuelType}.");
            }

            if (tank.Capacity + amount > tank.Capacity)
            {
                throw new InvalidOperationException($"Tank with ID {tankId} cannot be refueled with {amount} units. Capacity exceeded.");
            }
        }
    }
}

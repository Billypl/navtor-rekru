using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavtorShiper.Entities;
using NavtorShiper.Repositories;

namespace NavtorShiper.Services
{
    public interface ITankerShipService
    {
        void RefuelTank(string imo, int tankId, FuelType fuelType, double amount);
        void EmptyTank(string imo, int tankId);
    }

    public class TankerShipService : ITankerShipService
    {
        private readonly IShipService _shipService;

        public TankerShipService(IShipService shipService)
        {
            _shipService = shipService;
        }

        public void RefuelTank(string imo, int tankId, FuelType fuelType, double amount)
        {
            ITank tank = GetTank(imo, tankId);
            tank.Refuel(fuelType, amount);
        }

        public void EmptyTank(string imo, int tankId)
        {
            ITank tank = GetTank(imo, tankId);
            tank.Empty();
        }

        private ITank GetTank(string imo, int tankId)
        {
            var ship = _shipService.GetById(imo);
            if (ship is null)
            {
                throw new ArgumentException($"Ship with IMO {imo} not found.");
            }
            if (ship is not TankerShip tankerShip)
            {
                throw new InvalidOperationException($"Ship with IMO {imo} is not a tanker ship.");
            }
            var tank = tankerShip.Tanks.FirstOrDefault(t => t.Id == tankId);
            if (tank is null)
            {
                throw new ArgumentException($"Tank with ID {tankId} not found in ship with IMO {imo}.");
            }
            return tank;
        }
    }
}

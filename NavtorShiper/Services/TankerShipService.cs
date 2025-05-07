using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavtorShiper.Entities;
using NavtorShiper.Repositories;
using NavtorShiper.Validators;

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
            Tank tank = GetTank(imo, tankId);
            FuelingValidator.ValidateRefuelingParameters(tank, fuelType, amount);
            tank.Refuel(fuelType, amount);
        }

        public void EmptyTank(string imo, int tankId)
        {
            Tank tank = GetTank(imo, tankId);
            tank.Empty();
        }

        private Tank GetTank(string imo, int tankId)
        {
            var ship = _shipService.GetById(imo);
            ShipValidator.ValidateShipType<TankerShip>(ship);
            var tank = (ship as TankerShip)!.Tanks.FirstOrDefault(t => t.Id == tankId);
            if (tank is null)
            {
                throw new ArgumentException($"Tank with ID {tankId} not found in ship with IMO {imo}.");
            }
            return tank;
        }
    }
}

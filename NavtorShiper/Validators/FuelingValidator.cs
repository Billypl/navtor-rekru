using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavtorShiper.Entities;

namespace NavtorShiper.Validators
{
    class FuelingValidator
    {
        public static void ValidateRefuelingParameters(Tank tank, FuelType fuelType, double amount)
        {
            if (fuelType != tank.Type && tank.Type != FuelType.None)
            {
                throw new InvalidOperationException($"Tank with ID {tank.Id} cannot be refueled with {fuelType}. Current type is {tank.Type}.");
            }
            if (amount <= 0)
            {
                throw new ArgumentException($"Wrong amount of fuel: {amount}.");
            }
            if (tank.CurrentLevel + amount > tank.Capacity)
            {
                throw new InvalidOperationException($"Tank with ID {tank.Id} cannot be refueled with {amount} units. Capacity exceeded.");
            }
        }
    }
}

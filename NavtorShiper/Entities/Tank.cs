using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavtorShiper.Entities
{
    public enum FuelType
    {
        Diesel,
        HeavyFuel,
        None
    }

    public class Tank
    {
        public int Id { get; }
        public FuelType Type { get; private set; }
        public double Capacity { get; }
        public double CurrentLevel { get; private set; }

        public Tank(int id, double capacity, FuelType type = FuelType.None, double currentLevel = 0)
        {
            Id = id;
            Capacity = capacity;
            Type = type;
            CurrentLevel = currentLevel;
        }

        public void Refuel(FuelType fuelType, double amount)
        {
            Type = fuelType;
            CurrentLevel += amount;
        }

        public void Empty()
        {
            Type = FuelType.None;
            CurrentLevel = 0;
        }

        public override string ToString()
        {
            return $"TANK - Id: {Id}, Type: {Type}, Capacity: {Capacity}L, CurrentLevel: {CurrentLevel}L";
        }
    }
}

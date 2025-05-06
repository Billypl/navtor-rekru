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

    public interface ITank
    {
        int Id { get; set; }
        void Refuel(FuelType fuelType, double amount);
        void Empty();
    }

    public class Tank : ITank
    {
        public Tank(int id, double capacity)
        {
            Id = id;
            Capacity = capacity;
        }

        public Tank(int id, FuelType type, double capacity, double currentLevel) : this(id, capacity)
        {
            Type = type;
            CurrentLevel = currentLevel;
        }

        public int Id { get; set; }
        public FuelType Type { get; private set; } = FuelType.None;
        public double Capacity { get; set; }
        public double CurrentLevel { get; private set; } = 0;

        public void Refuel(FuelType fuelType, double amount)
        {
            ValidateRefuelingParameters(fuelType, amount);
            Type = fuelType;
            CurrentLevel += amount;
        }

        public void Empty()
        {
            Type = FuelType.None;
            CurrentLevel = 0;
        }

        private void ValidateRefuelingParameters(FuelType fuelType, double amount)
        {
            if (fuelType != Type && CurrentLevel != 0)
            {
                throw new InvalidOperationException($"Tank with ID {Id} cannot be refueled with {fuelType}. Current type is {Type}.");
            }

            if (amount < 0)
            {
                throw new ArgumentException("Tank cannot be refueled with a negative amount of fuel.");
            }

            if (CurrentLevel + amount > Capacity)
            {
                throw new InvalidOperationException($"Tank with ID {Id} cannot be refueled with {amount} units. Capacity exceeded.");
            }
        }

        public override string ToString()
        {
            return $"TANK - Id: {Id}, Type: {Type}, Capacity: {Capacity}L, CurrentLevel: {CurrentLevel}L";
        }
    }
}

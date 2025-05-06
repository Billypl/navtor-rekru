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
        HeavyFuel
    }
    public class Tank(int id, FuelType type, double capacity, double currentLevel)
    {
        public int Id { get; set; } = id;
        public FuelType Type { get; set; } = type;
        public double Capacity { get; set; } = capacity;
        public double CurrentLevel { get; set; } = currentLevel;

        public override string ToString()
        {
            return $"TANK - Id: {Id}, Type: {Type}, Capacity: {Capacity}L, CurrentLevel: {CurrentLevel}L\n";
        }
    }
}

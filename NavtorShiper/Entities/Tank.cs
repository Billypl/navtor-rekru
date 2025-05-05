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
    public class Tank(int id, double capacity, double currentLevel, FuelType type)
    {
        public int Id { get; set; } = id;
        public double Capacity { get; set; } = capacity;
        public double CurrentLevel { get; set; } = currentLevel;
        public FuelType Type { get; set; } = type;
    }
}

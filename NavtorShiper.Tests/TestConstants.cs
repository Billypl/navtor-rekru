using NavtorShiper.Entities;

namespace NavtorShiper.Tests
{
    public class TestConstants
    {
        // ships
        public const string ValidImo1 = "9074729";
        public const string ValidImo2 = "8814275";
        public const string InvalidImo = "9074728";

        // fuels
        public const int ExistingTankId = 1;
        public const int NonExistingTankId = 999;
        public const double DefaultCapacity = 1000;

        public const double HalfCapacityRefuelAmount = 500;
        public const double MoreThanHalfCapacityRefuelAmount = 501;
        public const double NegativeRefuelAmount = -500;

        public const FuelType DefaultFuelType = FuelType.Diesel;
        public const FuelType OtherFuelType = FuelType.HeavyFuel;
    }
}

using Microsoft.AspNetCore.SignalR;
using NavtorShiper.Entities;

namespace NavtorShiper.Web.DTOs
{
    public class ShipDto
    {
        public string ImoNumber { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public List<Passenger>? Passengers { get; set; }
        public List<Tank>? Tanks { get; set; }

        public static ShipDto MapToDto(Ship ship)
        {
            var dto = new ShipDto
            {
                ImoNumber = ship.ImoNumber,
                Type = ship.Type.ToString(),
                Name = ship.Name,
                Length = ship.Length,
                Width = ship.Width
            };

            switch (ship)
            {
                case PassengerShip passengerShip:
                    dto.Passengers = passengerShip.Passengers;
                    break;
                case TankerShip tankerShip:
                    dto.Tanks = tankerShip.Tanks;
                    break;
            }

            return dto;
        }

        public static IEnumerable<ShipDto> MapToDtos(IEnumerable<Ship> ships)
        {
            return ships.Select(MapToDto);
        }

        public static Ship MapToEntity(ShipDto dto)
        {
            return Enum.Parse<ShipType>(dto.Type, ignoreCase: true) switch
            {
                ShipType.Passenger => new PassengerShip(dto.ImoNumber, dto.Name, dto.Length, dto.Width, dto.Passengers),
                ShipType.Tanker => new TankerShip(dto.ImoNumber, dto.Name, dto.Length, dto.Width, dto.Tanks),
                _ => throw new ArgumentException($"Invalid dto type: {dto.Type}")
            };
        }


    }
}

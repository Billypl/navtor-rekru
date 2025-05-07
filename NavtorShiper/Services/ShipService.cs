using NavtorShiper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavtorShiper.Repositories;

namespace NavtorShiper.Services
{
    public interface IShipService
    {
        IEnumerable<Ship> GetAll();
        Ship GetById(string imo);
        void Add(Ship ship);
        void Delete(string imo);
    }

    public class ShipService : IShipService
    {
        private readonly IShipRepository _shipRepository;

        public ShipService(IShipRepository shipRepository)
        {
            _shipRepository = shipRepository;
        }

        public IEnumerable<Ship> GetAll()
        {
            return _shipRepository.GetAll();
        }

        public Ship GetById(string imo)
        {
            var ship = _shipRepository.GetById(imo);
            if (ship is null)
            {
                throw new ArgumentException($"Ship with IMO {imo} not found.");
            }
            return ship;
        }

        public void Add(Ship ship)
        {
            if (_shipRepository.GetById(ship.ImoNumber) is not null)
            {
                throw new ArgumentException($"Ship with IMO {ship.ImoNumber} already exists.");
            }
            _shipRepository.Add(ship);
        }

        public void Delete(string imo)
        {
            _shipRepository.Delete(GetById(imo));
        }
    }
}

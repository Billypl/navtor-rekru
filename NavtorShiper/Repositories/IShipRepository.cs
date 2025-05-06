using NavtorShiper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavtorShiper.Repositories
{
    public interface IShipRepository
    {
        IEnumerable<Ship> GetAll();
        Ship? GetById(string imo);
        void Add(Ship ship);
        bool Delete(string id);
    }
}

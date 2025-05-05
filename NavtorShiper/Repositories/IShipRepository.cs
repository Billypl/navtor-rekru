using NavtorShiper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavtorShiper.Repositories
{
    interface IShipRepository
    {
        IEnumerable<Ship> GetAll();
        Ship? GetById(string id);
        void Add(Ship ship);
        bool Update(Ship ship);
        bool Delete(string id);

    }
}

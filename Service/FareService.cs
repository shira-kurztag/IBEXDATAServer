using DB;
using IBEXDATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class FareService : IFareService
    {
        public readonly IFareDB _FareDB;

        public FareService(IFareDB FareDB)
        {
            _FareDB = FareDB;
            //_loggerFactory = loggerFactory;
            //_logger = _loggerFactory.GetLogger("file");
        }
        public IEnumerable<Fare> GetFares()
        {

            //_logger.Log("hiiiiiiiiiii");
            return _FareDB.GetFares();
        }
        public IEnumerable<Fare> DeleteFare(int id)
        {
            return _FareDB.DeleteFare(id);
        }
        public IEnumerable<Fare> UpdateFare(string fareName, int fareId)
        {
            return _FareDB.UpdateFare(fareName, fareId);
        }
        public async Task<IEnumerable<Fare>> filterFare(string FareName)
        {

            return await _FareDB.filterFare(FareName);

        }
        public IEnumerable<Fare> AddFare(Fare newFare)
        {
            return _FareDB.AddFare(newFare);
        }
        public void UpdateFareAmount(int fareId, int fareAmount)
        {
            _FareDB.UpdateFareAmount(fareId, fareAmount);
        }

    }
}

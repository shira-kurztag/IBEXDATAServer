using IBEXDATA.Models;

namespace Service
{
    public interface IFareService
    {
        IEnumerable<Fare> AddFare(Fare newFare);
        IEnumerable<Fare> DeleteFare(int id);
        Task<IEnumerable<Fare>> filterFare(string FareName);
        IEnumerable<Fare> GetFares();
        IEnumerable<Fare> UpdateFare(string fareName, int fareId);
        void UpdateFareAmount(int fareId, int fareAmount);
    }
}
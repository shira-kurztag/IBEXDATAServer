using IBEXDATA.Models;

namespace DB
{
    public interface IFareDB
    {
        IEnumerable<Fare> AddFare(Fare newFare);
        IEnumerable<Fare> DeleteFare(int id);
        Task<IEnumerable<Fare>> filterFare(string FareName);
        IEnumerable<Fare> GetFares();
        IEnumerable<Fare> UpdateFare(string fareText, int fareId);
        void UpdateFareAmount(int fareId, int fareAmount);
    }
}
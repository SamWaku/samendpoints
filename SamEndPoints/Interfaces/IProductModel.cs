using SamEndPoints.Models;

namespace SamEndpoints.SamEndPoints.Interfaces
{
    public interface IProductModel
    {
        //any class that implements this interface should have these things
        int Id { get; set;}
        string? Title { get; set; }
        bool? HasBeenOrdered { get; }
        //signature lines
        void ShipItem(User user);
    }
}
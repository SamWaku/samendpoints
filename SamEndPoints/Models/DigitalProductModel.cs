using SamEndpoints.SamEndPoints.Interfaces;
using SamEndPoints.Models;

namespace SamEndpoints.SamEndPoints.Models
{
    public class DigitalProductModel : IProductModel
    {
        public int Id { get; set;}
        public string? Title { get; set; }

        public bool? HasBeenOrdered {get; private set;}

        public void ShipItem(User user)
        {
            Console.WriteLine($"Simulating order for Digital Product of title {Title} to {user}");
        }

    }
}
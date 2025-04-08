using SamEndpoints.SamEndPoints.Interfaces;

namespace SamEndPoints.Models;

public class ProductModels : IProductModel
{
    public int Id { get; set;}
    public string? Title { get; set; }
    public bool? HasBeenOrdered { get;  private set; }

    public void ShipItem(User user)
    {
        if (HasBeenOrdered == false)
        {
            Console.WriteLine($"Simulate shipping of Book titled {Title} to { user.LastName}");
            // HasBeeCompleted = true;
        }
    }
}
using SamEndpoints.SamEndPoints.Interfaces;
using SamEndPoints.Models;

namespace SamEndPoints.SamEndPoints.Responses;

public class ProductResponse : IProductModel
{
    public int Id { get; set; }
    public string? Title { get; set; }

    public bool? HasBeenOrdered => throw new NotImplementedException();

    public void ShipItem(User user)
    {
        Console.WriteLine("Let's just log something");
    }
}

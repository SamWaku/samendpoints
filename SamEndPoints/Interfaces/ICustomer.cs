namespace SamEndpoints.SamEndPoints.Interfaces
{
    public interface ICustomer
    {
         IEnumerable<IOrder> PreviousOrders { get; }

         DateTime DateJoined { get;}
         DateTime? LastOrder {get;}
    }
}
namespace SamEndpoints.SamEndPoints.Interfaces
{
    public interface IOrder
    {
        DateTime dateTime{ get; set; }
        decimal Cost{ get; set; }
    }
}
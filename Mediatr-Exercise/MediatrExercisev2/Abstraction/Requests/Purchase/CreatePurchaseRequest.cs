using MediatrExercisev2.Domain.Entities.CustomerClass;

namespace MediatrExercisev2.Abstraction.Requests.Purchase
{
    public class CreatePurchaseRequest
    {
        public required Guid CustomerID { get; set; }
        public required Guid ProductID { get; set; }
    }
}

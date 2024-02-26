using MediatrExercisev2.Domain.Entities.CustomerClass;
using MediatrExercisev2.Domain.Entities.ItemClass;

namespace MediatrExercisev2.Domain.Entities.PurchaseClass
{
    public class Purchase
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ItemId { get; set; }

    }
}

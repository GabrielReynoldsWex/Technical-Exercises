namespace MediatrExercisev2.Abstraction.Responses.Purchase
{
    public class CreatePurchaseDTO
    {
        public CreatePurchaseDTO(Guid id) {
            this.Id = id;
        }

        public Guid Id { get; set; }
    }
}

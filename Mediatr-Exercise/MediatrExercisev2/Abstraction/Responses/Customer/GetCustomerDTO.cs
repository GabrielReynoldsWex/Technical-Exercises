namespace MediatrExercisev2.Abstraction.Responses.Customer
{
    public class GetCustomerDTO
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string ContactNumber { get; set; }
        public required bool CustomerDiscount { get; set; }
    }
}

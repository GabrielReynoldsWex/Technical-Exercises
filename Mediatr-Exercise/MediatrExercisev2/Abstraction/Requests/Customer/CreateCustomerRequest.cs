namespace MediatrExercisev2.Abstraction.Requests.Customer
{
    public class CreateCustomerRequest
    {
        public required string Name { get; set; }
        public required string ContactNumber { get; set; }
        public required string CustomerDiscount {  get; set; }
    }
}

namespace MediatrExercisev2.Abstraction.Responses.Customer
{
    public class CreateCustomerDTO
    {
        public CreateCustomerDTO(Guid id) 
        {
            this.Id = id;
        }
        public Guid Id { get; set; }
    }
}

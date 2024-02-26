namespace MediatrExercisev2.Domain.Entities.CustomerClass

{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public bool CustomerDiscount { get; set; }

        public Customer(string name, string contactNumber, bool customerDiscount) 
        {
            Name = name;
            ContactNumber = contactNumber;
            CustomerDiscount = customerDiscount;
        }
    }
}

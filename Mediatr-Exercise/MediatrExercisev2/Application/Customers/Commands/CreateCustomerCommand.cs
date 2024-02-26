using MediatR;
using MediatrExercisev2.Abstraction.Responses.Customer;
using MediatrExercisev2.Domain.Entities.CustomerClass;
using MediatrExercisev2.Repository.Context;

namespace MediatrExercisev2.Application.Customers.Commands
{
    public class CreateCustomerCommand : IRequest<CreateCustomerDTO>
    {
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public bool CustomerDiscount { get; set; }

        public CreateCustomerCommand(string name, string contactNumber, bool customerDiscount)
        {
            Name = name;
            ContactNumber = contactNumber;
            CustomerDiscount = customerDiscount;
        }
    }

    public static class CreateCustomerCommandExtension
    {
        public static Customer CreateCustomer(this CreateCustomerCommand command)
        {
            var Customer = new Customer(command.Name, command.ContactNumber, command.CustomerDiscount);
            return Customer;
        }
    }

    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CreateCustomerDTO>
    {
        private readonly ApplicationDbContext _dbcontext;

        public CreateCustomerCommandHandler(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<CreateCustomerDTO> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = request.CreateCustomer();

            await _dbcontext.Customers.AddAsync(customer, cancellationToken);
            await _dbcontext.SaveChangesAsync();

            return new CreateCustomerDTO(customer.Id);
        }
    }
}
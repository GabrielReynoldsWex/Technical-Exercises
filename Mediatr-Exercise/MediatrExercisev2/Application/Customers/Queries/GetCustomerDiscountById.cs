using MediatR;
using MediatrExercisev2.Abstraction.Responses.Customer;
using MediatrExercisev2.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace MediatrExercisev2.Application.Customers.Queries
{
    public class GetCustomerDiscountByIdQuery : IRequest<GetCustomerDiscountDTO> 
    { 
        public string CustomerId { get; set; }

        public GetCustomerDiscountByIdQuery(string customerId)
        {
            CustomerId = customerId;
        }

        public GetCustomerDiscountByIdQuery() { }
    }

    public class GetCustomerDiscountByIdQueryHandler : IRequestHandler<GetCustomerDiscountByIdQuery, GetCustomerDiscountDTO>
    {
        private readonly ApplicationDbContext _dbcontext;

        public GetCustomerDiscountByIdQueryHandler(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<GetCustomerDiscountDTO> Handle(GetCustomerDiscountByIdQuery request, CancellationToken cancellationToken)
        {

            Guid id = Guid.Parse(request.CustomerId);

            // Where Customer ID equals tempGuid variable, select customer name and discount
            var customer = await _dbcontext.Customers
                .Where(c => c.Id == id)
                .Select(c => new {c.Name, c.CustomerDiscount})
                .FirstOrDefaultAsync();

            if (customer == null) {
                throw new Exception("No such Customer found");
            }
           
            var result = new GetCustomerDiscountDTO();

            result.Name = customer.Name;
            result.CustomerDiscount = customer.CustomerDiscount;

            return result;
        }
    }
}
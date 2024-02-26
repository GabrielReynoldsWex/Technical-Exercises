using MediatR;
using MediatrExercisev2.Abstraction.Responses.Customer;
using MediatrExercisev2.Domain.Entities.CustomerClass;
using MediatrExercisev2.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace MediatrExercisev2.Application.Customers.Queries
{
    public class GetCustomersQuery : IRequest<IList<GetCustomerDTO>> { }

    public static class GetUsersQueryExtension
    {
        public static GetCustomerDTO MapTo(this Customer customer)
        {
            return new GetCustomerDTO
            {
                Id = customer.Id,
                Name = customer.Name,
                ContactNumber = customer.ContactNumber,
                CustomerDiscount = customer.CustomerDiscount
            };
        }
    }

    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, IList<GetCustomerDTO>>
    {
        private readonly ApplicationDbContext _dbContext;
        public GetCustomersQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<GetCustomerDTO>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _dbContext.Customers.ToListAsync();
            var List = new List<GetCustomerDTO>();
            foreach (var item in customers)
            {
                var customer = item.MapTo();
                List.Add(customer);
            }
            return List;
        }
    }
}

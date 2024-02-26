using MediatR;
using MediatrExercisev2.Abstraction.Responses.Purchase;
using MediatrExercisev2.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace MediatrExercisev2.Application.Purchases.Queries
{
    public class GetPurchasesByCustomerIdQuery : IRequest<IList<GetPurchaseDTO>>
    {
        public string CustomerId { get; set; }
        public bool CustomerDiscount { get; }
        public string? CategoryFilter { get; set; }
        public float PriceFilter { get; set; }

        public GetPurchasesByCustomerIdQuery(string customerId, bool customerDiscount)
        {
            CustomerId = customerId;
            CustomerDiscount = customerDiscount;
            CategoryFilter = null;
            PriceFilter = 0;
        }

        public GetPurchasesByCustomerIdQuery(string customerId, bool customerDiscount, string categoryFilter, float priceFilter)
        {
            CustomerId = customerId;
            CustomerDiscount = customerDiscount;
            CategoryFilter = categoryFilter;
            PriceFilter = priceFilter;
        }
    }

    public class GetPurchaseByCustomerIdQueryHandler : IRequestHandler<GetPurchasesByCustomerIdQuery, IList<GetPurchaseDTO>>
    {
        private readonly ApplicationDbContext _dbcontext;

        public GetPurchaseByCustomerIdQueryHandler(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IList<GetPurchaseDTO>> Handle(GetPurchasesByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            // Grab Customer ID
            Guid customerId = Guid.Parse(request.CustomerId);

            // Assemble Base Query
            var query = _dbcontext.Purchases
                .Where(p => p.CustomerId == customerId)
                .Join(_dbcontext.Items,
                    p => p.ItemId,
                    i => i.Id,
                    (p, i) => new
                    {
                        p.Id,
                        p.ItemId,
                        i.Name,
                        i.Category,
                        i.Price
                    });

            // Apply filters if set
            if(request.CategoryFilter != null)
                query = query.Where(i => i.Category.ToLower() ==  request.CategoryFilter.ToLower());

            if(request.PriceFilter != 0) 
                query = query.Where(i => i.Price <= request.PriceFilter);

            // Call query
            var purchaseItems = await query.ToListAsync();
            var result = new List<GetPurchaseDTO>();

            // Calculate prices and convert to DTO object, add to and return list of results
            foreach (var purchase in purchaseItems)
            {
                var price = "";

                if (request.CustomerDiscount)
                    price = (purchase.Price / 2).ToString("0.00");
                else
                    price = purchase.Price.ToString("0.00");

                var item = new GetPurchaseDTO(
                    purchase.Id,
                    purchase.ItemId,
                    purchase.Name,
                    purchase.Category,
                    price);
                result.Add(item);
            }

            return result;          
        }          
    }
}
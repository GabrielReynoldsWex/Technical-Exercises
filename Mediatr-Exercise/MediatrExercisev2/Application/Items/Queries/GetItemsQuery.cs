using MediatR;
using MediatrExercisev2.Abstraction.Responses.Item;
using MediatrExercisev2.Domain.Entities.ItemClass;
using MediatrExercisev2.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace MediatrExercisev2.Application.Items.Queries
{
    public class GetItemsQuery : IRequest<IList<GetItemDTO>> { }

    public static class GetItemsQueryExtension
    {
        public static GetItemDTO MapTo(this Item item)
        {
            return new GetItemDTO
            {
                Id = item.Id,
                Name = item.Name,
                Category = item.Category,
                Price = item.Price.ToString()
            };
        }
    }

    public class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, IList<GetItemDTO>>
    {
        private readonly ApplicationDbContext _dbcontext;

        public GetItemsQueryHandler(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IList<GetItemDTO>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
        {
            var items = await _dbcontext.Items.ToListAsync();
            var List = new List<GetItemDTO>();
            foreach (var product in items)
            {
                var item = product.MapTo();
                List.Add(item);
            }
            return List;
        }
    }
}
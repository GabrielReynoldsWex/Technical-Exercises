using MediatR;
using MediatrExercisev2.Abstraction.Responses.Item;
using MediatrExercisev2.Domain.Entities.ItemClass;
using MediatrExercisev2.Repository.Context;

namespace MediatrExercisev2.Application.Items.Commands
{
    public class CreateItemCommand : IRequest<CreateItemDTO>
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public float Price { get; set; }

        public CreateItemCommand(string name, string category, float price)
        {
            Name = name;
            Category = category;
            Price = price;
        }
    }

    public static class CreateItemCommandExtension
    {
        public static Item CreateItem(this CreateItemCommand command)
        {
            var Item = new Item(command.Name, command.Category, command.Price);
            return Item;
        }
    }

    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, CreateItemDTO>
    {
        public readonly ApplicationDbContext _dbcontext;

        public CreateItemCommandHandler(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<CreateItemDTO> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            var item = request.CreateItem();

            await _dbcontext.Items.AddAsync(item, cancellationToken);
            await _dbcontext.SaveChangesAsync();

            return new CreateItemDTO(item.Id);
        }
    }
}

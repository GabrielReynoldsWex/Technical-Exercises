using MediatR;
using MediatrExercisev2.Abstraction.Responses.Purchase;
using MediatrExercisev2.Domain.Entities.PurchaseClass;
using MediatrExercisev2.Repository.Context;

namespace MediatrExercisev2.Application.Purchases.Commands
{
    public class CreatePurchaseCommand : IRequest<CreatePurchaseDTO>
    {
        public Guid CustomerID { get; set; }
        public Guid ItemID { get; set; }

        public CreatePurchaseCommand(Guid customerID, Guid itemID)
        {
            CustomerID = customerID;
            ItemID = itemID;
        }
    }
    public static class CreatePurchaseCommandExtension
    {
        public static Purchase CreatePurchase(this CreatePurchaseCommand command)
        {
            var purchase = new Purchase();

            purchase.CustomerId = command.CustomerID;
            purchase.ItemId = command.ItemID;

            return purchase;
        }
    }
    public class CreatePurchaseCommandHandler : IRequestHandler<CreatePurchaseCommand, CreatePurchaseDTO>
    {
        private readonly ApplicationDbContext _dbcontext;

        public CreatePurchaseCommandHandler(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<CreatePurchaseDTO> Handle(CreatePurchaseCommand request, CancellationToken cancellationToken)
        {
            var purchase = request.CreatePurchase();

            await _dbcontext.Purchases.AddAsync(purchase, cancellationToken);
            await _dbcontext.SaveChangesAsync();

            return new CreatePurchaseDTO(purchase.Id);
        }
    }
}
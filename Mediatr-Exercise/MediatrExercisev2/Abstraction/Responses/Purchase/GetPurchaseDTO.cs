namespace MediatrExercisev2.Abstraction.Responses.Purchase
{
    public class GetPurchaseDTO
    {
        public Guid PurchaseId { get; set; }

        public Guid ItemId { get; set; }

        public string ItemName { get; set; }

        public string ItemCategory {  get; set; }

        public string ItemPrice { get; set; }

        public GetPurchaseDTO(Guid purchaseId, Guid itemId, string itemName, string itemCategory, string itemPrice) 
        {
            PurchaseId = purchaseId;
            ItemId = itemId;
            ItemName = itemName;
            ItemCategory = itemCategory;
            ItemPrice = itemPrice;
        }
    }
}

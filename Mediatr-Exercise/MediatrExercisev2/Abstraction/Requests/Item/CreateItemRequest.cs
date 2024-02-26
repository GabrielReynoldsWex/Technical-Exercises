namespace MediatrExercisev2.Abstraction.Requests.Item
{
    public class CreateItemRequest
    {
        public required string Name { get; set; }
        public required string Category { get; set; }
        public required string Price { get; set; }
    }
}

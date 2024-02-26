namespace MediatrExercisev2.Abstraction.Responses.Item
{
    public class CreateItemDTO
    {
        public CreateItemDTO(Guid id) {
            this.Id = id;
        }
        public Guid Id { get; set; }
    }
}

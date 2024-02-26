namespace MediatrExercisev2.Domain.Entities.ItemClass
{
    public class Item
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public float Price { get; set; }

        public Item(string name, string category, float price) { 
            Name = name;
            Category = category;
            Price = price;          
        }
    }
}
                    
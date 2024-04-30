namespace HotChocolateExercise
{
    public class Query
    {
        public Book GetBook()
        {
            return new Book
            {
                Title = "The Hobbit",
                Author = new Author
                {
                    Name = "J.R.R. Tolkien"
                }
            };
        }
    }
}

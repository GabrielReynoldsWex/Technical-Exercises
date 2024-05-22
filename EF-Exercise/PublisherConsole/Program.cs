using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

using (PubContext context = new PubContext())
{
    context.Database.EnsureCreated();
}

//GetAuthors();
//AddAuthor();
//GetAuthors();

AddAuthorWithBooks();
GetAuthorsWithBooks();

void GetAuthors()
{
    using var context = new PubContext();
    var authors = context.Authors.ToList();
    foreach (var author in authors)
    {
        Console.WriteLine($"{author.FirstName} {author.LastName}");
    }
}

void AddAuthor()
{
    var author = new Author() { FirstName = "Julie", LastName = "Tester" };
    using var context = new PubContext();
    context.Authors.Add(author);
    context.SaveChanges();
}

void AddAuthorWithBooks()
{
    var author = new Author() { FirstName = "Julie", LastName = "Lerman" };
    author.Books.Add(new Book()
    {
        Title = "Programming Entity Framework,",
        PublishDate = new DateOnly(2009, 1, 1),
    });
    author.Books.Add(new Book()
    {
        Title = "Programming Entity Framework 2nd Ed",
        PublishDate = new DateOnly(2010, 8, 1),
    });
    using var context = new PubContext();
    context.Authors.Add(author);
    context.SaveChanges();
}

void GetAuthorsWithBooks()
{
    using var context = new PubContext();
    var authors = context.Authors.Include(a => a.Books).ToList();
    foreach (var author in authors)
    {
        Console.WriteLine(author.FirstName + " " + author.LastName);
        foreach (var book in author.Books)
        {
            Console.WriteLine("  " + book.Title);
        }
    }
}
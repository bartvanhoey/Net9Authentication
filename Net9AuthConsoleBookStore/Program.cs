using Microsoft.Extensions.DependencyInjection;
using Net9AuthConsoleBookStore.Services.Authors;
using Net9AuthConsoleBookStore.Services.Authors.Dtos;
using Net9AuthConsoleBookStore.Services.Books;
using Net9AuthConsoleBookStore.Services.Books.Dtos;
using Net9AuthConsoleBookStore.Services.Http;

// First set up the Dependency Injection System to register the Book Http Service and the BookService
var services = new ServiceCollection();

// Registration the Book HttpService to the Dependency Injection system
services.AddTransient<IHttpService<BookDto, CreateBookDto, UpdateBookDto, GetBooksDto, Guid>,
    HttpService<BookDto, CreateBookDto, UpdateBookDto, GetBooksDto, Guid>>();

services.AddTransient<IHttpService<AuthorDto, CreateAuthorDto, UpdateAuthorDto, GetAuthorsDto, Guid>,
    HttpService<AuthorDto, CreateAuthorDto, UpdateAuthorDto, GetAuthorsDto, Guid>>();

// Registration the BookService to the Dependency Injection system
services.AddTransient<IBookService, BookService>();
// services.AddTransient<IAuthorService, AuthorService>();

// Get the BookService from the Dependency Injection System
// The Book service becomes via its constructor the Book HttpService (Constructor Dependency Injection) and is ready to use.
try
{
    var bookService = services.BuildServiceProvider().GetRequiredService<IBookService>();

    // Create a book
    var createdBook =
        await bookService.CreateBookAsync(new CreateBookDto("New Book3", BookType.Adventure, DateTime.Now, 10.0f));

    // Get a list of books => The result should be a list of 3 books. 
    var books = await bookService.GetBooksAsync();


    var updatedBook = await bookService.UpdateBookAsync(new UpdateBookDto(createdBook!.Id, "New Book3 Updated",
        BookType.ScienceFiction, DateTime.Now.AddMonths(5), 10.0f));
    var getBook = await bookService.GetBookAsync(createdBook.Id.ToString());
    await bookService.DeleteBookAsync(createdBook.Id);

    // Authors
    const string authorApiUrl = "https://localhost:7247/api/app/author";
    services.AddTransient<IHttpService<AuthorDto, CreateAuthorDto, UpdateAuthorDto, GetAuthorsDto, Guid>,
        HttpService<AuthorDto, CreateAuthorDto, UpdateAuthorDto, GetAuthorsDto, Guid>>();

    services.AddTransient<IAuthorService, AuthorService>(options
        => new AuthorService(
            options
                .GetRequiredService<
                    IHttpService<AuthorDto, CreateAuthorDto, UpdateAuthorDto, GetAuthorsDto, Guid>>(),
            authorApiUrl));

    var authorService = services.BuildServiceProvider().GetService<IAuthorService>();

    var getAuthors1 = await authorService.GetAuthorsAsync();
    var createdAuthor =
        await authorService.CreateAuthorAsync(new CreateAuthorDto("Author 5", DateTime.Now.AddYears(-50), "Short Bio"));
    var updatedAuthor = await authorService.UpdateAuthorAsync(new UpdateAuthorDto(createdAuthor!.Id, "Author 5 Updated",
        DateTime.Now.AddYears(-5), "ShortBio Updated"));
    var getAuthor = await authorService.GetAuthorAsync(createdAuthor.Id.ToString());
    var getAuthors2 = await authorService.GetAuthorsAsync();
    await authorService.DeleteAuthorAsync(createdAuthor.Id);


    Console.ReadLine(); // Set here a breakpoint to see the results
}
catch (Exception e)
{
    Console.WriteLine(e);
    throw;
}


namespace Net9AuthConsoleBookStore.Services.Books.Dtos;

public class CreateBookDto(string? name, BookType bookType, DateTime publishDate, float price)
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public BookType Type { get; set; } = bookType;
    public float Price { get; set; } = price;
    public DateTime PublishDate { get; set; } = publishDate;
    public string? Name { get; set; } = name;
}
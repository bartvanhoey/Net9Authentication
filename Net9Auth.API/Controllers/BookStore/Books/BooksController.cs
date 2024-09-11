using Microsoft.AspNetCore.Mvc;
using Net9Auth.API.Controllers.BookStore.Books.Dtos;

using Net9Auth.Shared.Infrastructure.Models;
using static Net9Auth.API.Controllers.BookStore.Books.BooksResolver;

namespace Net9Auth.API.Controllers.BookStore.Books;

[Route("api/app/book")]
[ApiController]
public class BooksController : ControllerBase
{
    [HttpGet]
    public PagedResultDto<BookDto> Get([FromQuery] GetBooksDto getBooksDto) =>
        new() { Items = BookItems, TotalCount = BookItems.Count };

    [HttpGet("{id}")]
    public BookDto? Get(Guid id) => BookItems.FirstOrDefault(x => x.Id == id);

    [HttpPost]
    public BookDto Create([FromBody] CreateBookDto createBookDto)
    {
        BookItems.Add(new BookDto(createBookDto.Name, createBookDto.Type, createBookDto.Price,
            createBookDto.PublishDate, createBookDto.Id));
        return BookItems.Single(x => x.Id == createBookDto.Id);
    }

    [HttpPut("{id}")]
    public BookDto? Put(Guid id, [FromBody] UpdateBookDto updateBookDto)
    {
        var bookDto = BookItems.FirstOrDefault(x => x.Id == id);
        if (bookDto == null) return bookDto;
        bookDto.Name = updateBookDto.Name;
        bookDto.Price = updateBookDto.Price;
        bookDto.PublishDate = updateBookDto.PublishDate;
        bookDto.Type = updateBookDto.Type;
        return bookDto;
    }

    [HttpDelete("{id}")]
    public void Delete(Guid id)
    {
        var bookDto = BookItems.FirstOrDefault(x => x.Id == id);
        if (bookDto != null) BookItems.Remove(bookDto);
    }
}
using Net9Auth.ConsoleBookStore.Services.Authors.Dtos;

namespace Net9Auth.ConsoleBookStore.Services.Authors;

public interface IAuthorService
{
    Task<IEnumerable<AuthorDto>> GetAuthorsAsync();
    Task<AuthorDto?> UpdateAuthorAsync(UpdateAuthorDto book);
    Task DeleteAuthorAsync(Guid bookDtoId);
    Task CreateManyAuthorsAsync(IEnumerable<CreateAuthorDto> bookDtos);
    Task<AuthorDto?> CreateAuthorAsync(CreateAuthorDto bookDto);
    Task<AuthorDto?> GetAuthorAsync(string bookId);
}
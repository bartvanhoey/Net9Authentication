using Microsoft.AspNetCore.Mvc;
using Net9Auth.API.Controllers.BookStore.Authors.Dtos;
using Net9Auth.API.Models.GenericHttp;
using static Net9Auth.API.Controllers.BookStore.Authors.AuthorsResolver;

namespace Net9Auth.API.Controllers.BookStore.Authors;

[Route("api/app/author")]
[ApiController]
public class AuthorsController : ControllerBase
{
    [HttpGet]
    public PagedResultDto<AuthorDto> Get() 
        => new() { Items = AuthorItems, TotalCount = AuthorItems.Count };

    [HttpGet("{id}")]
    public AuthorDto? Get(Guid id) => AuthorItems.FirstOrDefault(x => x.Id == id);
        
    [HttpPost]
    public AuthorDto Create([FromBody] CreateAuthorDto createAuthorDto)
    {
        AuthorItems.Add(new AuthorDto(createAuthorDto.Name, createAuthorDto.BirthDate,createAuthorDto.ShortBio, createAuthorDto.Id));
        return AuthorItems.Single(x => x.Id == createAuthorDto.Id);
    }
        
    [HttpPut("{id}")]
    public AuthorDto? Put(Guid id, [FromBody] UpdateAuthorDto updateAuthorDto)
    {
        var authorDto = AuthorItems.FirstOrDefault(x => x.Id == id);
        if (authorDto == null) return authorDto;
        authorDto.Name = updateAuthorDto.Name;
        authorDto.BirthDate = updateAuthorDto.BirthDate;
        authorDto.ShortBio = updateAuthorDto.ShortBio;
        return authorDto;
    }
        
    [HttpDelete("{id}")]
    public void Delete(Guid id)
    {
        var authorDto = AuthorItems.FirstOrDefault(x => x.Id == id);
        if (authorDto != null) AuthorItems.Remove(authorDto);
    }
}
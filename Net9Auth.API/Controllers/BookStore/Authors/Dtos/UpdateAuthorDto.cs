﻿namespace Net9Auth.API.Controllers.BookStore.Authors.Dtos;

public class UpdateAuthorDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }

    public DateTime BirthDate { get; set; }

    public string? ShortBio { get; set; }
}
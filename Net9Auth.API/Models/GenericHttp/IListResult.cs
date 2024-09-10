namespace Net9Auth.API.Models.GenericHttp;

public interface IListResult<T>
{
    IReadOnlyList<T> Items { get; set; }
}
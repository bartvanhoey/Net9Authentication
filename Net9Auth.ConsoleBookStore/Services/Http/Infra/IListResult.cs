namespace Net9Auth.ConsoleBookStore.Services.Http.Infra;

public interface IListResult<T>
{
    IReadOnlyList<T> Items { get; set; }
}
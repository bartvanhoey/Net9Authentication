namespace Net9Auth.ConsoleBookStore.Services.Http.Infra;

public interface IPagedResult<T> : IListResult<T>, IHasTotalCount
{
}
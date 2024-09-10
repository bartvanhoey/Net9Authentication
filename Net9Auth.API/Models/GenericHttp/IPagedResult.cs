namespace Net9Auth.API.Models.GenericHttp;

public interface IPagedResult<T>: IListResult<T>, IHasTotalCount
{
        
}
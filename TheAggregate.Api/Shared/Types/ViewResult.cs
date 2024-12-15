namespace TheAggregate.Api.Shared.Types;

public class ViewResult<T>
{
    public T? Data { get; set; }
    public List<string> Errors { get; set; } = [];
    public bool HasErrors => Errors?.Count > 0;
}
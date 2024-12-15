namespace TheAggregate.Api.Features.Identity.RegisterUser;

public class RegisterUserResponse
{
    public bool Success { get; set; }
    public List<string> Errors { get; set; } = [];
    public bool HasErrors => Errors?.Count > 0;
    public string? Message { get; set; }
}
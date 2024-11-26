using FastEndpoints;

namespace TheAggregate.Api.Features.Test;

public class TestEndpoint : Endpoint<TestRequest, TestResponse>
{
    public override void Configure() => Get("/test/{Name}");
    
    public override async Task HandleAsync(TestRequest request, CancellationToken cancellationToken)
    {
        await SendAsync(new TestResponse { Message = $"Hello {request.Name}" }, cancellation: cancellationToken);
    }
}
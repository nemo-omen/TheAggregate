using System.ServiceModel.Syndication;
using System.Xml;
using FluentResults;
using Polly;
using TheAggregate.Shared.Infrastructure;

namespace TheAggregate.Api.Features.SyndicationFeeds;

public interface IFeedReader
{
    Task<Result<SyndicationFeed>> ReadAsync(string url, CancellationToken cancellationToken = default);
}


public class FeedReader : IFeedReader
{
    private readonly ResiliencePipeline<HttpResponseMessage> _retryPipeline;
    private readonly HttpClient _httpClient;
    public FeedReader(HttpClient httpClient, ResiliencePipeline<HttpResponseMessage>? retryPipeline = null)
    {
        _httpClient = httpClient;
        if (retryPipeline is null)
        {
            retryPipeline = RetryPipelineFactory.CreateDefaultPipeline();
        }
        
        _retryPipeline = retryPipeline;
    }
    
    /// <summary>
    /// Reads a syndication feed from the specified URL asynchronously. This method uses a retry policy
    /// to handle transient errors and will retry up to 3 times with an exponential backoff delay.
    /// </summary>
    /// <param name="url">The URL of the syndication feed to read.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A result containing the syndication feed or an error message.</returns>
    public async Task<Result<SyndicationFeed>> ReadAsync(string url, CancellationToken cancellationToken = default)
    {
        // Validate input
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new ArgumentException("URL cannot be null or empty.", nameof(url));
        }
        
        if (!Uri.TryCreate(url, UriKind.Absolute, out var uriResult) ||
            (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
        {
            throw new ArgumentException("The provided URL is not valid. Only HTTP and HTTPS are supported.", nameof(url));
        }

        // Fetch the feed
        try
        {
            // using var httpClient = new HttpClient();
            var pipelineResponse = await _retryPipeline.ExecuteAsync(async token =>
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/111.0.0.0 Safari/537.36");
                // request.Headers.Referrer = new Uri("https://www.google.com"); // Some servers check referrer
                request.Headers.Accept.ParseAdd("text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");

                var response = await _httpClient.SendAsync(request, cancellationToken);
                return response;
            }, cancellationToken);

            if (!pipelineResponse.IsSuccessStatusCode)
            {
                var result = Result.Fail<SyndicationFeed>($"Failed to fetch the feed for {url}.")
                    .WithError(new ExceptionalError("HttpResponseException",
                        new Exception(pipelineResponse.ReasonPhrase)));
                return result;
            }

            await using var stream = await pipelineResponse.Content.ReadAsStreamAsync(cancellationToken);
            
            var settings = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Parse, // Allow DTDs
                XmlResolver = null // Disable external resource resolution for security
            };
            
            using var reader = XmlReader.Create(stream, settings);

            cancellationToken.ThrowIfCancellationRequested();

            var feed = SyndicationFeed.Load(reader);
            return Result.Ok(feed);
        }
        // Handle exceptions
        catch (HttpRequestException e)
        {
            return Result.Fail<SyndicationFeed>($"Failed to fetch the feed for {url}")
                .WithError(new ExceptionalError("HttpRequestException", e));
        }
        catch (XmlException e)
        {
            return Result.Fail<SyndicationFeed>($"Failed to parse the feed for {url}")
                .WithError(new ExceptionalError("XmlException", e));
        }
        catch (OperationCanceledException e)
        {
            return Result.Fail<SyndicationFeed>($"The operation was canceled while fetching the feed for {url}")
                .WithError(new ExceptionalError("OperationCanceledException", e));
        }
        catch(ArgumentNullException e)
        {
            return Result.Fail<SyndicationFeed>($"The feed for {url} is null")
                .WithError(new ExceptionalError("ArgumentNullException", e));
        }
        catch (Exception e)
        {
            return Result.Fail<SyndicationFeed>("An unexpected error occurred while fetching the feed")
                .WithError(new ExceptionalError("Exception", e));
        }
    }
}
using FluentResults;
using MediatR;

namespace TheAggregate.Api.Features.Feeds.GetFeedCategories;

public record GetFeedCategoriesCommand : IRequest<Result<List<FeedCategoryResponse>>> {}
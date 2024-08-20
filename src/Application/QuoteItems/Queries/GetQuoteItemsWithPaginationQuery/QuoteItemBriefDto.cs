using Orange.Domain.Entities;
using Orange.Domain.Enums;

namespace Orange.Application.QuoteItems.Queries.GetQuoteItemsWithPaginationQuery;
public class QuoteItemBriefDto
{
    public string? Title { get; init; }
    public string? Note { get; init; }
    public RatingLevel? Rating { get; init; }
    public string? Comments { get; init; }
    public string? Source { get; init; }
    public string? Author { get; init; }
    public string? ReferenceUrl { get; init; }
    public string? Tags { get; init; }
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<QuoteItem, QuoteItemBriefDto>();
        }
    }
}

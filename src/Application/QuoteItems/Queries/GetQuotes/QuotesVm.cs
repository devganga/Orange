using Orange.Domain.Entities;
using Orange.Domain.Enums;

namespace Orange.Application.QuoteItems.Queries.GetQuotes;
public class QuotesVm
{
    public string? Title { get; init; }
    public string? Note { get; init; }
    public RatingLevel? Rating { get; init; }
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<QuoteItem, QuotesVm>();
            //CreateMap<QuoteItem, QuoteItemBriefDto>().ForMember(d => d.Rating, opt => opt.MapFrom(s => (s.Rating ?? 0)));
        }
    }
}

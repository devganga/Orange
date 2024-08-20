using Orange.Application.Common.Interfaces;
using Orange.Application.Common.Mappings;
using Orange.Application.Common.Models;

namespace Orange.Application.QuoteItems.Queries.GetQuoteItemsWithPaginationQuery;

public record GetQuoteItemsWithPaginationQueryQuery : IRequest<PaginatedList<QuoteItemBriefDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;

}

public class GetQuoteItemsWithPaginationQueryQueryValidator : AbstractValidator<GetQuoteItemsWithPaginationQueryQuery>
{
    public GetQuoteItemsWithPaginationQueryQueryValidator()
    {
        RuleFor(x => x.PageNumber)
           .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}

public class GetQuoteItemsWithPaginationQueryQueryHandler : IRequestHandler<GetQuoteItemsWithPaginationQueryQuery, PaginatedList<QuoteItemBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetQuoteItemsWithPaginationQueryQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<QuoteItemBriefDto>> Handle(GetQuoteItemsWithPaginationQueryQuery request, CancellationToken cancellationToken)
    {
        return await _context.QuoteItems
           //.Where(x => x.ListId == request.ListId)
           .OrderByDescending(x => x.Id)
           .ProjectTo<QuoteItemBriefDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}

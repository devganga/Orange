using Orange.Application.Common.Interfaces;


namespace Orange.Application.QuoteItems.Queries.GetQuotes;

public record GetQuotesQuery : IRequest<IEnumerable<QuotesVm>>
{
    //public int PageNumber { get; init; } = 1;
    //public int PageSize { get; init; } = 10;
}

public class GetQuotesQueryValidator : AbstractValidator<GetQuotesQuery>
{
    public GetQuotesQueryValidator()
    {
        //RuleFor(x => x.PageNumber)
        //   .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        //RuleFor(x => x.PageSize)
        //    .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}

public class GetQuotesQueryHandler : IRequestHandler<GetQuotesQuery, IEnumerable<QuotesVm>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetQuotesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<QuotesVm>> Handle(GetQuotesQuery request, CancellationToken cancellationToken)
    {
        return await _context.QuoteItems
           //.Where(x => x.ListId == request.ListId)
           .OrderByDescending(x => x.Id)
           .ProjectTo<QuotesVm>(_mapper.ConfigurationProvider)
           .ToListAsync(cancellationToken);
        //.PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}

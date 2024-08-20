using Orange.Application.Common.Interfaces;
using Orange.Domain.Entities;
using Orange.Domain.Enums;
using Orange.Domain.Events;

namespace Orange.Application.QuoteItems.Commands.CreateQuoteItem;

public record CreateQuoteItemCommand : IRequest<int>
{
    public string? Title { get; init; }
    public string? Note { get; init; }
    public RatingLevel? Rating { get; init; }
    public string? Comments { get; init; }
    public string? Source { get; init; }
    public string? Author { get; init; }
    public string? ReferenceUrl { get; init; }
    public string? Tags { get; init; }
}

public class CreateQuoteItemCommandValidator : AbstractValidator<CreateQuoteItemCommand>
{
    private readonly IApplicationDbContext _context;
    public CreateQuoteItemCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Title)
           .NotEmpty();

        RuleFor(v => v.Note)
          .NotEmpty()
          .MustAsync(BeUniqueNote)
                .WithMessage("'{PropertyName}' must be unique.")
                .WithErrorCode("Unique");

    }

    public async Task<bool> BeUniqueNote(string? note, CancellationToken cancellationToken)
    {
        return await _context.QuoteItems
            .AllAsync(l => l.Note != note, cancellationToken);
    }
}

public class CreateQuoteItemCommandHandler : IRequestHandler<CreateQuoteItemCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateQuoteItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateQuoteItemCommand request, CancellationToken cancellationToken)
    {
        var entity = new QuoteItem
        {
            Author = request.Author,
            Title = request.Title,
            Note = request.Note,
            Comments = request.Comments,
            Source = request.Source,
            Rating = request.Rating ?? null,
            ReferenceUrl = request.ReferenceUrl,
            Tags = request.Tags,
        };

        entity.AddDomainEvent(new QuoteItemCreatedEvent(entity));

        _context.QuoteItems.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}

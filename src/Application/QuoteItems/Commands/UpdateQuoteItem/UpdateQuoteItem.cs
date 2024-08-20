using Orange.Application.Common.Interfaces;
using Orange.Domain.Enums;

namespace Orange.Application.QuoteItems.Commands.UpdateQuoteItem;

public record UpdateQuoteItemCommand : IRequest
{
    public int Id { get; init; }
    public string? Title { get; init; }
    public string? Note { get; init; }
    public RatingLevel? Rating { get; init; }
    public string? Comments { get; init; }
    public string? Source { get; init; }
    public string? Author { get; init; }
    public string? ReferenceUrl { get; init; }
    public string? Tags { get; init; }
}

public class UpdateQuoteItemCommandValidator : AbstractValidator<UpdateQuoteItemCommand>
{
    private readonly IApplicationDbContext _context;
    public UpdateQuoteItemCommandValidator(IApplicationDbContext context)
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

    public async Task<bool> BeUniqueNote(UpdateQuoteItemCommand model, string? note, CancellationToken cancellationToken)
    {
        return await _context.QuoteItems
            .Where(l => l.Id != model.Id)
            .AllAsync(l => l.Note != note, cancellationToken);
    }
}

public class UpdateQuoteItemCommandHandler : IRequestHandler<UpdateQuoteItemCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateQuoteItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateQuoteItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.QuoteItems
             .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Title ??= request.Title;
        entity.Author ??= request.Author;
        entity.Title ??= request.Title;
        entity.Note ??= request.Note;
        entity.Comments ??= request.Comments;
        entity.Source ??= request.Source;
        entity.Rating ??= request.Rating;
        entity.ReferenceUrl ??= request.ReferenceUrl;
        entity.Tags ??= request.Tags;

        await _context.SaveChangesAsync(cancellationToken);
    }
}

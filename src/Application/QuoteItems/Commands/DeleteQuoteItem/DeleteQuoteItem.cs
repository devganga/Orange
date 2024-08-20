using Orange.Application.Common.Interfaces;
using Orange.Domain.Events;

namespace Orange.Application.QuoteItems.Commands.DeleteQuoteItem;

public record DeleteQuoteItemCommand(int Id) : IRequest
{
}

public class DeleteQuoteItemCommandValidator : AbstractValidator<DeleteQuoteItemCommand>
{
    public DeleteQuoteItemCommandValidator()
    {
    }
}

public class DeleteQuoteItemCommandHandler : IRequestHandler<DeleteQuoteItemCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteQuoteItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteQuoteItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.QuoteItems
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.QuoteItems.Remove(entity);

        //entity.AddDomainEvent(new TodoItemDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}

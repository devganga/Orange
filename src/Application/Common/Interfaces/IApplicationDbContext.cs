using Orange.Domain.Entities;

namespace Orange.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<QuoteItem> QuoteItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

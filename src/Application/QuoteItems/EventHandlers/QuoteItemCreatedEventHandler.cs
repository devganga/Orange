using Blusky.Services;
using Microsoft.Extensions.Logging;
using Orange.Domain.Events;

namespace Orange.Application.QuoteItems.EventHandlers;
public class QuoteItemCreatedEventHandler : INotificationHandler<QuoteItemCreatedEvent>
{
    private readonly ILogger<QuoteItemCreatedEventHandler> _logger;
    private readonly ITelegramService _telegram;

    public QuoteItemCreatedEventHandler(ILogger<QuoteItemCreatedEventHandler> logger, IBluskyService bluskyService)
    {
        _logger = logger;
        _telegram = bluskyService.TelegramService;
    }
    public Task Handle(QuoteItemCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Orange Domain Event: {DomainEvent}", notification.GetType().Name);
        _telegram.SendAsync(notification.Item.Note);
        return Task.CompletedTask;
    }
}

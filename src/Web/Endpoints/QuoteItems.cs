
using Orange.Application.Common.Models;
using Orange.Application.QuoteItems.Commands.CreateQuoteItem;
using Orange.Application.QuoteItems.Commands.DeleteQuoteItem;
using Orange.Application.QuoteItems.Commands.UpdateQuoteItem;
using Orange.Application.QuoteItems.Queries.GetQuotes;
using Orange.Application.QuoteItems.Queries.GetQuoteItemsWithPaginationQuery;

namespace Orange.Web.Endpoints;

public class QuoteItems : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
           .RequireAuthorization()
           .MapGet(GetQuoteItems)
           .MapGet(GetQuoteItemsWithPagination, "Pagination")
           .MapPost(CreateQuoteItem)
           .MapPut(UpdateQuoteItem, "{id}")
           //.MapPut(UpdateTodoItemDetail, "UpdateDetail/{id}")
           .MapDelete(DeleteQuoteItem, "{id}");
    }

    public Task<IEnumerable<QuotesVm>> GetQuoteItems(ISender sender, [AsParameters] GetQuotesQuery query)
    {
        return sender.Send(query);
    }

    public Task<PaginatedList<QuoteItemBriefDto>> GetQuoteItemsWithPagination(ISender sender, [AsParameters] GetQuoteItemsWithPaginationQueryQuery query)
    {
        return sender.Send(query);
    }

    public Task<int> CreateQuoteItem(ISender sender, CreateQuoteItemCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IResult> UpdateQuoteItem(ISender sender, int id, UpdateQuoteItemCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteQuoteItem(ISender sender, int id)
    {
        await sender.Send(new DeleteQuoteItemCommand(id));
        return Results.NoContent();
    }
}

namespace Orange.Domain.Entities;
public class QuoteItem : BaseAuditableEntity
{
    public string? Title { get; set; }
    public string? Note { get; set; }
    public RatingLevel? Rating { get; set; }
    public string? Comments { get; set; }
    public string? Source { get; set; }
    public string? Author { get; set; }
    public string? ReferenceUrl { get; set; }
    public string? Tags { get; set; }
}

namespace Contacts.Infrastructure.Models;

public abstract class EntityBase
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
}

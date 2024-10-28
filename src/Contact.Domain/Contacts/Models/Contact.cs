using Contacts.Infrastructure.Models;

namespace Contacts.Domain.Contacts.Models;
public class Contact : EntityBase
{
    public string Name { get; set; }
    public ContactPhone Phone { get; set; }
    public ContactEmail Email { get; set; }
}

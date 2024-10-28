namespace Contacts.Domain.Contacts.Models;
public class ContactEmail
{
    public string Address { get; set; }

    public override string ToString() => Address;
}

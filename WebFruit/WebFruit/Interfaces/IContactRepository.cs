using WebFruit.Models;
using WebFruit.DTOs;

namespace WebFruit.Interfaces
{
    public interface IContactRepository
    {
        public void AddContact(ContactDTO contact);
        IEnumerable<Contact> GetAllContacts();
        IEnumerable<EmailSubscriptions> GetAllSubscriber();
    }
}

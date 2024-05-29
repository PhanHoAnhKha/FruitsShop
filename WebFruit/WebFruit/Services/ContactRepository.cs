using WebFruit.DTOs;
using WebFruit.Interfaces;
using WebFruit.Models;
using WebFruit.Data;

namespace WebFruit.Services
{
    public class ContactRepository : IContactRepository
    {
        private readonly FruitDbContext _dbContext;
        public ContactRepository(FruitDbContext context)
        {
            _dbContext = context;
        }
        public void AddContact(ContactDTO contactDto)
        {
            try
            {
                var contact = new Contact
                {
                    Name = contactDto.Name,
                    Email = contactDto.Email,
                    Message = contactDto.Message
                };

                _dbContext.Contacts.Add(contact);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the contact", ex);
            }
        }

        public IEnumerable<Contact> GetAllContacts()
        {
            return _dbContext.Contacts.ToList();
        }
        public IEnumerable<EmailSubscriptions> GetAllSubscriber()
        {
            return _dbContext.EmailSubscriptions.ToList();
        }
    }
}

using MyBlog.DataAccess.Abstract;
using MyBlog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.DataAccess.Concrete
{
    public class EfContactRepository : IContactRepository
    {
        private CvContext _context;
        public EfContactRepository(CvContext context)
        {
            _context = context;
        }

        public void AddContact(Contact entity)
        {
            _context.Contacts.Add(entity);
            _context.SaveChanges();
        }

        public void DeleteContact(int contactId)
        {
            var query = _context.Contacts.FirstOrDefault(p => p.Id == contactId);
            if (query!=null)
            {
                _context.Contacts.Remove(query);
                _context.SaveChanges();
            }
        }

        public IQueryable<Contact> GetAll()
        {
            return _context.Contacts;
        }

        public Contact GetById(int contactId)
        {
            return _context.Contacts.FirstOrDefault(p => p.Id == contactId);
        }
    }
}

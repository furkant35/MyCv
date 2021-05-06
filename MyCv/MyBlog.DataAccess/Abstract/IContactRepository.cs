using MyBlog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.DataAccess.Abstract
{
    public interface IContactRepository
    {
        Contact GetById(int contactId);
        IQueryable<Contact> GetAll();
        void AddContact(Contact entity);
        void DeleteContact(int contactId);
    }
}

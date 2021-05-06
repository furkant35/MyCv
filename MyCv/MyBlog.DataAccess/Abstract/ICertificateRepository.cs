using MyBlog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.DataAccess.Abstract
{
    public interface ICertificateRepository
    {
        Certificate GetById(int certificateId);
        IQueryable<Certificate> GetAll();
        void AddCertificate(Certificate entity);
        void UpdateCertificate(Certificate entity);
        void DeleteCertificate(int certificateId);
    }
}

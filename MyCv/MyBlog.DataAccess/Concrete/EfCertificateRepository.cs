using MyBlog.DataAccess.Abstract;
using MyBlog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.DataAccess.Concrete
{
    public class EfCertificateRepository : ICertificateRepository
    {
        private CvContext _context;
        public EfCertificateRepository(CvContext context)
        {
            _context = context;
        }

        public void AddCertificate(Certificate entity)
        {
            _context.Certificates.Add(entity);
            _context.SaveChanges();
        }

        public void DeleteCertificate(int certificateId)
        {
            var query = _context.Certificates.FirstOrDefault(p => p.Id == certificateId);
            if (query!=null)
            {
                _context.Certificates.Remove(query);
                _context.SaveChanges();
            }
        }

        public IQueryable<Certificate> GetAll()
        {
            return _context.Certificates;
        }

        public Certificate GetById(int certificateId)
        {
            return _context.Certificates.First(p => p.Id == certificateId);
        }

        public void UpdateCertificate(Certificate entity)
        {
            var certificate = GetById(entity.Id);
            if (certificate!=null)
            {
                certificate.CertificateName = entity.CertificateName;
                certificate.CertificateDate = entity.CertificateDate;
                certificate.ImageUrl = entity.ImageUrl;
            }
            _context.SaveChanges();
        }
    }
}

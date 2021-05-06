using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Entities
{
    public class Certificate
    {
        public int Id { get; set; }
        public string CertificateName { get; set; }
        public string CertificateDate { get; set; }
        public string ImageUrl { get; set; }
    }
}

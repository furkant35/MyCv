using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Entities
{
    public class CvContext:DbContext
    {
        public CvContext(DbContextOptions<CvContext> options) : base(options)
        {

        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Login> Logins { get; set; }
    }
}

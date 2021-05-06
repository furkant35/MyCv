using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyBlog.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        [Required]
        public string ContactName { get; set; }
        [Required]
        public string ContactEmail { get; set; }
        [Required]
        public string ContactMessage { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyBlog.Entities
{
    public class Login
    {
        [Key]
        public int LoginId { get; set; }
        [Required]
        public string LoginName { get; set; }
        [Required]
        public string LoginPassword { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lesson25.Models
{
    public class Contact
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> PhoneNumbers { get; set; }
    }
}
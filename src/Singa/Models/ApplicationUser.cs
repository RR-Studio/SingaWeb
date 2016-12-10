using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Singa.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string SecondaryEmail { get; set; }
        public string Surname { get; set; }
        public string PostalCode { get; set; }
        public string Institute { get; set; }
        public int Address { get; set; }
        public string Department { get; set; }
        public string CountryCode { get; set; }        
        private string GivenName { get; set; }
        private string Title { get; set; }

        //from article: Version is timestamp of the alert
        private DateTime Version { get; set; }

    }
}

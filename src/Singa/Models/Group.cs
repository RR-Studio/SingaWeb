using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Singa.Models
{
    public class Group
    {
        public Guid GroupId { get; set; }
        public string Name { get; set; }
        public List<string> GroupMessages_temp { get; set; }  // TODO replace with class
        public List<string> Users_temp { get; set; }
        public List<string> Owners_temp { get; set; }

    }
}

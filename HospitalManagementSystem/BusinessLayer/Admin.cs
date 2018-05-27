using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Admin
    {
        public string Name { set; get; }
        public string Email { set; get; }
        public int Contact { set; get; }
        public string Address { set; get; }
        public DateTime DateOfBirth { set; get; }
        public string Password { set; get; }
    }
}

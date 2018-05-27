using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Person
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Address { set; get; }
        public int Contactno { set; get; }
        public DateTime JoiningDate { set; get; }
        public double Salary { set; get; }
        public string Designation { set; get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Patient
    {
        public int id { set; get; }
        public string Name { set; get; }
        public string Address { set; get; }
        public int Contactno { set; get; }
        public string Status { set; get; }
        public int RoomNo { set; get; }
        public DateTime DOB { set; get; }
        public string Gender { set; get; }
        public string Condition { set; get; }
        public int Roomno { set; get; }
        public DateTime? Admitdate { set; get; }
        public DateTime? Releasedate { set; get; }
        public int Total { set; get; }
    }
}

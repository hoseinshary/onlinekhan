using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasleGhalam.DomainClasses.Entities
{
    public class Majors
    {
        public Majors()
        {
            StudentMajorlists = new HashSet<StudentMajorlist>();
        }
        public int Id { get; set; }
        public string Course { get; set; }
        public int Code { get; set; }
        public string MajorTitle { get; set; }
        public int AdmissionFirst { get; set; }
        public int AdmissionSecond { get; set; }
        public string Woman { get; set; }
        public string Man { get; set; }
        public string University { get; set; }
        public ICollection<StudentMajorlist> StudentMajorlists { get; set; }
    }
}

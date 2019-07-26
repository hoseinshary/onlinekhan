using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NasleGhalam.Common;

namespace NasleGhalam.DomainClasses.Entities
{
    public class Resume
    {
        public int Id { get; set; }

        public string Branch { get; set; }
            
        public DateTime CreationDateTime { get; set; }

        public string Name { get; set; }
        public string Family { get; set; }

        public string FatherName { get; set; }

        public string IdNumber { get; set; }

        public string NationalNo { get; set; }

        public bool Gender { get; set; }

        public string Phone { get; set; }

        public string Mobile { get; set; }

        public string CityBorn { get; set; }

        public DateTime Birthday { get; set; }

        public bool Marriage { get; set; }

        public string Religion { get; set; }

        public string Address { get; set; }

        public City City { get; set; }

        public int CityId { get; set; }

        public string PostCode { get; set; }

        public string FatherJob { get; set; }

        public Degree FatherDegree { get; set; }

        public string FatherPhone { get; set; }

        public string MotherJob { get; set; }

        public Degree MotherDegree { get; set; }

        public string MotherPhone { get; set; }
        public string WifeJob { get; set; }

        public Degree WifeDegree { get; set; }

        public string WifePhone { get; set; }

        public string Reagent1 { get; set; }
        public string RelationReagent1 { get; set; }

        public string JobReagent1 { get; set; }
        public string PhoneReagent1 { get; set; }
        public string AddressReagent1 { get; set; }

        public string Reagent2 { get; set; }
        public string RelationReagent2 { get; set; }

        public string JobReagent2 { get; set; }
        public string PhoneReagent2 { get; set; }
        public string AddressReagent2 { get; set; }


        public bool HaveEducationCertificate { get; set; }
        public bool HaveAnotherCertificate { get; set; }
        public string AnotherCertificate { get; set; }
        public string EducationCertificateJSON { get; set; }
        public bool HavePublication { get; set; }

        public int NumberOfPublication { get; set; }

        public string PublicationJSON { get; set; }

        public bool HaveTeachingResume { get; set; }

        public int NumberOfTeachingYear { get; set; }

        public string TeachingResumeJSON { get; set; }

        public bool TeachingOrPublishingRequest1 { get; set; }
        public Maghta MaghtaRequest1 { get; set; }
        public KindRequest KindRequestRequest1 { get; set; }

        public string LessonNameRequest1 { get; set; }

        public bool TeachingOrPublishingRequest2 { get; set; }
        public Maghta MaghtaRequest2 { get; set; }
        public KindRequest KindRequestRequest2 { get; set; }

        public string LessonNameRequest2 { get; set; }

        public bool RequestForAdvice { get; set; }

        public Maghta MaghtaAdvice { get; set; }

        public string Description { get; set; }




    }
}

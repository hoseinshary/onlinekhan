using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels.Resume
{
	public class ResumeViewModel
	{
		[Display(Name = "")]
		public int Id { get; set; }


		[Display(Name = "")]
		public string Branch { get; set; }


		[Display(Name = "")]
		public DateTime CreationDateTime { get; set; }


		[Display(Name = "")]
		public string Name { get; set; }


		[Display(Name = "")]
		public string Family { get; set; }


		[Display(Name = "")]
		public string FatherName { get; set; }


		[Display(Name = "")]
		public string IdNumber { get; set; }


		[Display(Name = "")]
		public string NationalNo { get; set; }


		[Display(Name = "")]
		public bool Gender { get; set; }


		[Display(Name = "")]
		public string Phone { get; set; }


		[Display(Name = "")]
		public string Mobile { get; set; }


		[Display(Name = "")]
		public string CityBorn { get; set; }


		[Display(Name = "")]
		public DateTime Birthday { get; set; }


		[Display(Name = "")]
		public bool Marriage { get; set; }


		[Display(Name = "")]
		public string Religion { get; set; }


		[Display(Name = "")]
		public string Address { get; set; }


		[Display(Name = "")]
		public int CityId { get; set; }


		[Display(Name = "")]
		public string PostCode { get; set; }


		[Display(Name = "")]
		public string FatherJob { get; set; }


		[Display(Name = "")]
		public Degree FatherDegree { get; set; }


		[Display(Name = "")]
		public string FatherPhone { get; set; }


		[Display(Name = "")]
		public string MotherJob { get; set; }


		[Display(Name = "")]
		public Degree MotherDegree { get; set; }


		[Display(Name = "")]
		public string MotherPhone { get; set; }


		[Display(Name = "")]
		public string WifeJob { get; set; }


		[Display(Name = "")]
		public Degree WifeDegree { get; set; }


		[Display(Name = "")]
		public string WifePhone { get; set; }


		[Display(Name = "")]
		public string Reagent1 { get; set; }


		[Display(Name = "")]
		public string RelationReagent1 { get; set; }


		[Display(Name = "")]
		public string JobReagent1 { get; set; }


		[Display(Name = "")]
		public string PhoneReagent1 { get; set; }


		[Display(Name = "")]
		public string AddressReagent1 { get; set; }


		[Display(Name = "")]
		public string Reagent2 { get; set; }


		[Display(Name = "")]
		public string RelationReagent2 { get; set; }


		[Display(Name = "")]
		public string JobReagent2 { get; set; }


		[Display(Name = "")]
		public string PhoneReagent2 { get; set; }


		[Display(Name = "")]
		public string AddressReagent2 { get; set; }


		[Display(Name = "")]
		public bool HaveEducationCertificate { get; set; }


		[Display(Name = "")]
		public bool HaveAnotherCertificate { get; set; }


		[Display(Name = "")]
		public string AnotherCertificate { get; set; }


		[Display(Name = "")]
		public string EducationCertificateJSON { get; set; }


		[Display(Name = "")]
		public bool HavePublication { get; set; }


		[Display(Name = "")]
		public int NumberOfPublication { get; set; }


		[Display(Name = "")]
		public string PublicationJSON { get; set; }


		[Display(Name = "")]
		public bool HaveTeachingResume { get; set; }


		[Display(Name = "")]
		public int NumberOfTeachingYear { get; set; }


		[Display(Name = "")]
		public string TeachingResumeJSON { get; set; }


		[Display(Name = "")]
		public bool TeachingOrPublishingRequest1 { get; set; }


		[Display(Name = "")]
		public Maghta MaghtaRequest1 { get; set; }


		[Display(Name = "")]
		public KindRequest KindRequestRequest1 { get; set; }


		[Display(Name = "")]
		public string LessonNameRequest1 { get; set; }


		[Display(Name = "")]
		public bool TeachingOrPublishingRequest2 { get; set; }


		[Display(Name = "")]
		public Maghta MaghtaRequest2 { get; set; }


		[Display(Name = "")]
		public KindRequest KindRequestRequest2 { get; set; }


		[Display(Name = "")]
		public string LessonNameRequest2 { get; set; }


		[Display(Name = "")]
		public bool RequestForAdvice { get; set; }


		[Display(Name = "")]
		public Maghta MaghtaAdvice { get; set; }


		[Display(Name = "")]
		public string Description { get; set; }


	}
}

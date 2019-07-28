import {
  Degree,
  Maghta,
  KindRequest,
  DegreeCertificate,
  TypeEducationCenter
} from "src/utilities/enumeration";

export default interface IResume {
  Id: number;
  Branch: string;
  Name: string;
  Family: string;
  FatherName: string;
  IdNumber: string;
  NationalNo: string;
  Gender: boolean;
  Phone: string;
  Mobile: string;
  CityBorn: string;
  Birthday: string;
  Marriage: boolean;
  Religion: string;
  Address: string;
  CityId: number;
  PostCode: string;
  FatherJob: string;
  FatherDegree: Degree;
  FatherPhone: string;
  MotherJob: string;
  MotherDegree: Degree;
  MotherPhone: string;
  PartnerJob: string;
  PartnerDegree: Degree;
  PartnerPhone: string;
  Reagent1: string;
  RelationReagent1: string;
  JobReagent1: string;
  PhoneReagent1: string;
  AddressReagent1: string;
  Reagent2: string;
  RelationReagent2: string;
  JobReagent2: string;
  PhoneReagent2: string;
  AddressReagent2: string;
  HaveAnotherCertificate: boolean;
  AnotherCertificate: string;
  HavePublication: boolean;
  NumberOfPublication: number;
  HaveTeachingResume: boolean;
  NumberOfTeachingYear: number;
  TeachingOrPublishingRequest1: boolean;
  MaghtaRequest1: Maghta;
  KindRequest1: KindRequest;
  LessonNameRequest1: string;
  TeachingOrPublishingRequest2: boolean;
  MaghtaRequest2: Maghta;
  KindRequest2: KindRequest;
  LessonNameRequest2: string;
  RequestForAdvice: boolean;
  MaghtaAdvice: Maghta;
  Description: string;
  Publications: Array<IPublication>;
  EducationCertificates: Array<IEducationCertificate>;
  TeachingResumes: Array<ITeachingResume>;
}

export const DefaultResume: IResume = {
  Id: 0,
  Branch: "",
  Name: "",
  Family: "",
  FatherName: "",
  IdNumber: "",
  NationalNo: "",
  Gender: false,
  Phone: "",
  Mobile: "",
  CityBorn: "",
  Birthday: "",
  Marriage: false,
  Religion: "",
  Address: "",
  CityId: 0,
  PostCode: "",
  FatherJob: "",
  FatherDegree: Degree.Karshenasi,
  FatherPhone: "",
  MotherJob: "",
  MotherDegree: Degree.Karshenasi,
  MotherPhone: "",
  PartnerJob: "",
  PartnerDegree: Degree.Karshenasi,
  PartnerPhone: "",
  Reagent1: "",
  RelationReagent1: "",
  JobReagent1: "",
  PhoneReagent1: "",
  AddressReagent1: "",
  Reagent2: "",
  RelationReagent2: "",
  JobReagent2: "",
  PhoneReagent2: "",
  AddressReagent2: "",
  HaveAnotherCertificate: false,
  AnotherCertificate: "",
  HavePublication: false,
  NumberOfPublication: 0,
  HaveTeachingResume: false,
  NumberOfTeachingYear: 0,
  TeachingOrPublishingRequest1: false,
  MaghtaRequest1: Maghta.Dah,
  KindRequestRequest1: KindRequest.Both,
  LessonNameRequest1: "",
  TeachingOrPublishingRequest2: false,
  MaghtaRequest2: Maghta.Dah,
  KindRequestRequest2: KindRequest.Both,
  LessonNameRequest2: "",
  RequestForAdvice: false,
  MaghtaAdvice: Maghta.Dah,
  Description: "",
  Publications: [],
  EducationCertificates: [],
  TeachingResumes: []
};

interface IPublication {
  Publisher: string;
  Grade: string;
  LessonName: string;
  KindRequest: KindRequest;
  Year: string;
  PublishedOrEdit: boolean;
}

interface IEducationCertificate {
  DegreeCertificate: DegreeCertificate;
  Subject: string;
  EducationCenterName: string;
  CityName: string;
  TypeEducationCenter: TypeEducationCenter;
  PublishedOrEdit: boolean;
  Year: string;
  Score: number;
}

interface ITeachingResume {
  School: string;
  LessonName: string;
  Grade: string;
  StartYear: string;
  EndYear: string;
}

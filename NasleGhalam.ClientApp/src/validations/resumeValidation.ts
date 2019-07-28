import { required, displayName, maxLength } from "src/plugins/vuelidate";
import { ValidationRuleset, validationMixin } from "vuelidate";
import IResume from "src/models/IResume";

type TResume = { resume: IResume; validationGroup: string[] };
const resumeValidations: ValidationRuleset<TResume> = {
  resume: {
    Branch: {
      displayName: displayName("شعبه"),
      maxLength: maxLength(50),
      required
    },
    Name: {
      displayName: displayName("نام"),
      maxLength: maxLength(150),
      required
    },
    Family: {
      displayName: displayName("نام خانوادگی"),
      maxLength: maxLength(150),
      required
    },
    FatherName: {
      displayName: displayName("نام پدر"),
      maxLength: maxLength(150),
      required
    },
    IdNumber: {
      displayName: displayName("شماره شناسنامه"),
      maxLength: maxLength(10),
      required
    },
    NationalNo: {
      displayName: displayName("کد ملی"),
      maxLength: maxLength(10),
      required
    },
    Gender: {
      displayName: displayName("جنسیت"),
      required
    },
    Phone: {
      displayName: displayName("تلفن"),
      maxLength: maxLength(8),
      required
    },
    Mobile: {
      displayName: displayName("موبایل"),
      maxLength: maxLength(10),
      required
    },
    CityBorn: {
      displayName: displayName("صادره"),
      maxLength: maxLength(50),
      required
    },
    Birthday: {
      displayName: displayName("تاریخ تولد"),
      maxLength: maxLength(50),
      required
    },
    Marriage: {
      displayName: displayName("وضعیت تاهل"),
      maxLength: maxLength(50),
      required
    },
    Religion: {
      displayName: displayName("وضعیت مذهب"),
      maxLength: maxLength(50),
      required
    },
    Address: {
      displayName: displayName("آدرس"),
      maxLength: maxLength(300),
      required
    },
    CityId: {
      displayName: displayName("شهر"),
      maxLength: maxLength(50),
      required
    },
    PostCode: {
      displayName: displayName("کد پستی"),
      maxLength: maxLength(10),
      required
    },
    FatherJob: {
      displayName: displayName("شغل پدر"),
      maxLength: maxLength(150)
    },
    FatherDegree: {
      displayName: displayName("مدرک پدر"),
      required
    },
    FatherPhone: {
      displayName: displayName("شماره تماس پدر"),
      maxLength: maxLength(10)
    },
    MotherJob: {
      displayName: displayName("شغل مادر"),
      maxLength: maxLength(150),
      required
    },
    MotherDegree: {
      displayName: displayName("مدرک مادر"),
      required
    },
    MotherPhone: {
      displayName: displayName("شماره تماس مادر"),
      maxLength: maxLength(10),
      required
    },
    PartnerJob: {
      displayName: displayName("شغل همسر"),
      maxLength: maxLength(150),
      required
    },
    PartnerDegree: {
      displayName: displayName("مدرک همسر"),
      required
    },
    PartnerPhone: {
      displayName: displayName("شماره تماس همسر"),
      maxLength: maxLength(10),
      required
    },
    Reagent1: {
      displayName: displayName("معرف 1"),
      maxLength: maxLength(50)
    },
    RelationReagent1: {
      displayName: displayName("نسبت معرف 1"),
      maxLength: maxLength(50)
    },
    JobReagent1: {
      displayName: displayName("شعل معرف 1"),
      maxLength: maxLength(50)
    },
    PhoneReagent1: {
      displayName: displayName("شماره تماس معرف 1"),
      maxLength: maxLength(10)
    },
    AddressReagent1: {
      displayName: displayName("آدرس معرف 1"),
      maxLength: maxLength(300)
    },
    Reagent2: {
      displayName: displayName("معرف 2"),
      maxLength: maxLength(50)
    },
    RelationReagent2: {
      displayName: displayName("نسبت معرف 2"),
      maxLength: maxLength(50)
    },
    JobReagent2: {
      displayName: displayName("شعل معرف 2"),
      maxLength: maxLength(50)
    },
    PhoneReagent2: {
      displayName: displayName("شماره تماس معرف 2"),
      maxLength: maxLength(10),
      required
    },
    AddressReagent2: {
      displayName: displayName("آدرس معرف 2"),
      maxLength: maxLength(300),
      required
    },
    HaveAnotherCertificate: {
      displayName: displayName("گزینش از ارگان دیگر"),
      required
    },
    AnotherCertificate: {
      displayName: displayName("نام ارگان"),
      maxLength: maxLength(50),
      required
    },
    HavePublication: {
      displayName: displayName("سابقه تالیف"),
      required
    },
    NumberOfPublication: {
      displayName: displayName("تعداد تالیفات"),
      required
    },
    HaveTeachingResume: {
      displayName: displayName("سابقه تدریس"),
      required
    },
    NumberOfTeachingYear: {
      displayName: displayName("تعداد سال سابقه تدریس"),
      required
    },
    TeachingOrPublishingRequest1: {
      displayName: displayName("تقاضای تدریس یا تالیف 1"),
      required
    },
    MaghtaRequest1: {
      displayName: displayName("پایه تقاضای 1"),
      required
    },
    KindRequest1: {
      displayName: displayName("نوع تقاضای 1"),
      required
    },
    LessonNameRequest1: {
      displayName: displayName("درس تقاضای 1"),
      maxLength: maxLength(50),
      required
    },
    TeachingOrPublishingRequest2: {
      displayName: displayName("تقاضای تدریس یا تالیف 2"),
      required
    },
    MaghtaRequest2: {
      displayName: displayName("پایه تقاضای 2"),
      required
    },
    KindRequest2: {
      displayName: displayName("نوع تقاضای 2"),
      required
    },
    LessonNameRequest2: {
      displayName: displayName("درس تقاضای 2"),
      maxLength: maxLength(50),
      required
    },
    RequestForAdvice: {
      displayName: displayName("درخواست برای مشاوره"),
      required
    },
    MaghtaAdvice: {
      displayName: displayName("پایه مشاوره"),
      required
    },
    Description: {
      displayName: displayName("توضیحات"),
      required
    }
  }
};

export { validationMixin, resumeValidations };

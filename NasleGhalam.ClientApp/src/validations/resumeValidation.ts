import {
  required,
  displayName,
  maxLength,
  requiredDdl,
  numeric,
  requiredIf
} from "src/plugins/vuelidate";
import { ValidationRuleset, validationMixin } from "vuelidate";
import IResume, {
  IPublication,
  IEducationCertificate,
  ITeachingResume
} from "src/models/IResume";

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
      numeric,
      required
    },
    NationalNo: {
      displayName: displayName("کد ملی"),
      maxLength: maxLength(10),
      numeric,
      required
    },
    Gender: {
      displayName: displayName("جنسیت"),
      required
    },
    Phone: {
      displayName: displayName("تلفن"),
      maxLength: maxLength(8),
      numeric,
      required
    },
    Mobile: {
      displayName: displayName("موبایل"),
      maxLength: maxLength(10),
      numeric,
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
    ProvinceId: {
      displayName: displayName("استان"),
      requiredDdl: requiredDdl(0)
    },
    CityId: {
      displayName: displayName("شهر"),
      requiredDdl: requiredDdl(0)
    },
    PostCode: {
      displayName: displayName("کد پستی"),
      maxLength: maxLength(10),
      numeric,
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
      numeric,
      maxLength: maxLength(10)
    },
    MotherJob: {
      displayName: displayName("شغل مادر"),
      maxLength: maxLength(150)
    },
    MotherDegree: {
      displayName: displayName("مدرک مادر"),
      required
    },
    MotherPhone: {
      displayName: displayName("شماره تماس مادر"),
      maxLength: maxLength(10),
      numeric
    },
    PartnerJob: {
      displayName: displayName("شغل همسر"),
      maxLength: maxLength(150),
      required: requiredIf((model: IResume) => {
        return model.Marriage;
      })
    },
    PartnerDegree: {
      displayName: displayName("مدرک همسر"),
      required: requiredIf((model: IResume) => {
        return model.Marriage;
      })
    },
    PartnerPhone: {
      displayName: displayName("شماره تماس همسر"),
      maxLength: maxLength(10),
      numeric,
      required: requiredIf((model: IResume) => {
        return model.Marriage;
      })
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
      numeric,
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
      numeric
    },
    AddressReagent2: {
      displayName: displayName("آدرس معرف 2"),
      maxLength: maxLength(300)
    },
    HaveAnotherCertificate: {
      displayName: displayName("گزینش از ارگان دیگر")
    },
    AnotherCertificate: {
      displayName: displayName("نام ارگان"),
      maxLength: maxLength(50),
      required: requiredIf((model: IResume) => {
        return model.HaveAnotherCertificate;
      })
    },
    HavePublication: {
      displayName: displayName("سابقه تالیف"),
      required
    },
    NumberOfPublication: {
      displayName: displayName("تعداد تالیفات"),
      numeric,
      required: requiredIf((model: IResume) => {
        return model.HavePublication;
      })
    },
    HaveTeachingResume: {
      displayName: displayName("سابقه تدریس"),
      required: requiredIf((model: IResume) => {
        return model.HavePublication;
      })
    },
    NumberOfTeachingYear: {
      displayName: displayName("تعداد سال سابقه تدریس"),
      numeric,
      required: requiredIf((model: IResume) => {
        return model.HaveTeachingResume;
      })
    },
    TeachingOrPublishingRequest1: {
      displayName: displayName("تقاضای تدریس یا تالیف 1"),
      required
    },
    MaghtaRequest1: {
      displayName: displayName("پایه تقاضای 1"),
      required: requiredIf((model: IResume) => {
        return model.TeachingOrPublishingRequest1;
      })
    },
    KindRequest1: {
      displayName: displayName("نوع تقاضای 1"),
      required: requiredIf((model: IResume) => {
        return model.TeachingOrPublishingRequest1;
      })
    },
    LessonNameRequest1: {
      displayName: displayName("درس تقاضای 1"),
      maxLength: maxLength(50),
      required: requiredIf((model: IResume) => {
        return model.TeachingOrPublishingRequest1;
      })
    },
    TeachingOrPublishingRequest2: {
      displayName: displayName("تقاضای تدریس یا تالیف 2"),
      required
    },
    MaghtaRequest2: {
      displayName: displayName("پایه تقاضای 2"),
      required: requiredIf((model: IResume) => {
        return model.TeachingOrPublishingRequest2;
      })
    },
    KindRequest2: {
      displayName: displayName("نوع تقاضای 2"),
      required: requiredIf((model: IResume) => {
        return model.TeachingOrPublishingRequest2;
      })
    },
    LessonNameRequest2: {
      displayName: displayName("درس تقاضای 2"),
      maxLength: maxLength(50),
      required: requiredIf((model: IResume) => {
        return model.TeachingOrPublishingRequest2;
      })
    },
    RequestForAdvice: {
      displayName: displayName("درخواست برای مشاوره"),
      required
    },
    MaghtaAdvice: {
      displayName: displayName("پایه مشاوره"),
      required: requiredIf((model: IResume) => {
        return model.RequestForAdvice;
      })
    },
    Description: {
      displayName: displayName("توضیحات"),
      maxLength: maxLength(300)
    }
  }
};

type TPublication = { publication: IPublication; validationGroup: string[] };
const publicationValidations: ValidationRuleset<TPublication> = {
  publication: {
    Publisher: {
      displayName: displayName("ناشر"),
      maxLength: maxLength(50),
      required
    },
    Grade: {
      displayName: displayName("مقطع"),
      maxLength: maxLength(50),
      required
    },
    LessonName: {
      displayName: displayName("درس"),
      maxLength: maxLength(50),
      required
    },
    KindRequest: {
      displayName: displayName("نوع کتاب"),
      required
    },
    Year: {
      displayName: displayName("سال"),
      required,
      numeric
    },
    PublishedOrEdit: {
      displayName: displayName("تالیف یا ویرایش"),
      required
    }
  }
};

type TEducationCertificate = {
  educationCertificate: IEducationCertificate;
  validationGroup: string[];
};
const educationCertificateValidations: ValidationRuleset<
  TEducationCertificate
> = {
  educationCertificate: {
    DegreeCertificate: {
      displayName: displayName("مدرک"),
      required
    },
    Subject: {
      displayName: displayName("رشته"),
      required
    },
    EducationCenterName: {
      displayName: displayName("مرکز تحصیلی"),
      maxLength: maxLength(50),
      required
    },
    CityName: {
      displayName: displayName("شهر"),
      maxLength: maxLength(50),
      required
    },
    TypeEducationCenter: {
      displayName: displayName("نوع"),
      required,
      numeric
    },
    Year: {
      displayName: displayName("سال"),
      required,
      numeric
    },
    Score: {
      displayName: displayName("معدل"),
      required,
      numeric
    }
  }
};

type TTeachingResume = {
  teachingResume: ITeachingResume;
  validationGroup: string[];
};
const teachingResumeValidations: ValidationRuleset<TTeachingResume> = {
  teachingResume: {
    School: {
      displayName: displayName("آموزشگاه"),
      maxLength: maxLength(50),
      required
    },
    LessonName: {
      displayName: displayName("رشته"),
      maxLength: maxLength(50),
      required
    },
    Grade: {
      displayName: displayName("مقطع"),
      maxLength: maxLength(50),
      required
    },
    StartYear: {
      displayName: displayName("سال شروع"),
      required
    },
    EndYear: {
      displayName: displayName("سال پایان"),
      required,
      numeric
    }
  }
};

export {
  validationMixin,
  resumeValidations,
  publicationValidations,
  educationCertificateValidations,
  teachingResumeValidations
};

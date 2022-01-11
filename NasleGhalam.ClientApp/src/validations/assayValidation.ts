import {
  required,
  displayName,
  maxLength,
  numeric,
  requiredDdl
} from "src/plugins/vuelidate";
import { ValidationRuleset, validationMixin } from "vuelidate";
import AssayCreate from "src/models/IAssay";

type TAssay = { assayCreate: AssayCreate; validationGroup: string[] };
const assayValidations: ValidationRuleset<TAssay> = {
  assayCreate: {
    Title: {
      displayName: displayName(" نام آزمون"),
      maxLength: maxLength(75),
      required
    },
    Time: {
      displayName: displayName("زمان به دقیقه"),
      numeric,
      required
    },
    LookupId_Importance: {
      displayName: displayName("اهمیت"),
      requiredDdl: requiredDdl(0)
    },
    LookupId_Type: {
      displayName: displayName("نوع"),
      requiredDdl: requiredDdl(0)
    },
    LookupId_QuestionType: {
      displayName: displayName("نوع سوالات"),
      requiredDdl: requiredDdl(0)
    },
    NumberOfVarient: {
      displayName: displayName("تعداد نسخه های آزمون"),
      requiredDdl: requiredDdl(0)
    },

    IsPublic: {
      displayName: displayName("قابل اجرا برای همه"),
      required
    },
    IsOnline: {
      displayName: displayName("آنلاین"),
      required
    },
    RandomOptions: {
      displayName: displayName("جابجایی گزینه ها"),
      required
    },
    RandomQuestion: {
      displayName: displayName("سوال تصادفی"),
      required
    },
    QuestionsRelocation: {
      displayName: displayName("جابجایی سوال")
    },
    FontSize: {
      displayName: displayName("سایز فونت"),
      numeric,
      required
    },
    Font: {
      displayName: displayName("فونت"),
      required
    },
    TwoPageInOne: {
      displayName: displayName("دو صفحه در یک صفحه")
    },
    HaveWhiteSpace: {
      displayName: displayName("جای چرک نویس")
    }
  }
};

export { validationMixin, assayValidations };

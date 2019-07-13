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
      displayName: displayName("نام"),
      maxLength: maxLength(75),
      required
    },
    Time: {
      displayName: displayName("زمان"),
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
    IsPublic: {
      displayName: displayName("قابل اجرا برای همه"),
      required
    },
    IsOnline: {
      displayName: displayName("آنلاین"),
      required
    },
    RandomOptions: {
      displayName: displayName("سوال تصادفی"),
      required
    },
    RandomQuestion: {
      displayName: displayName("گزینه های تصادفی"),
      required
    }
  }
};

export { validationMixin, assayValidations };

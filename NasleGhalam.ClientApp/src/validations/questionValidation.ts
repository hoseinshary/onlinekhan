import {
  required,
  displayName,
  maxLength,
  numeric,
  requiredDdl
} from "src/plugins/vuelidate";
import { ValidationRuleset, validationMixin } from "vuelidate";
import IQuestion from "src/models/IQuestion";

type TQuestion = { question: IQuestion; validationGroup: string[] };
const questionValidations: ValidationRuleset<TQuestion> = {
  question: {
    Context: {
      displayName: displayName("متن")
    },
    QuestionNumber: {
      displayName: displayName("شماره سوال"),
      numeric,
      required
    },
    LookupId_QuestionType: {
      displayName: displayName("نوع سوال"),
      numeric,
      requiredDdl: requiredDdl(0)
    },
    QuestionPoint: {
      displayName: displayName("نمره"),
      numeric,
      required
    },
    LookupId_QuestionHardnessType: {
      displayName: displayName("درجه سختی"),
      numeric,
      requiredDdl: requiredDdl(0)
    },
    AnswerNumber: {
      displayName: displayName("شماره گزینه صحیح"),
      numeric
    },
    LookupId_RepeatnessType: {
      displayName: displayName("درجه تکرار"),
      numeric,
      requiredDdl: requiredDdl(0)
    },
    UseEvaluation: {
      displayName: displayName("ارزیابی")
    },
    IsStandard: {
      displayName: displayName("استاندارد")
    },
    LookupId_AuthorType: {
      displayName: displayName("نوع طراح"),
      numeric,
      requiredDdl: requiredDdl(0)
    },
    AuthorName: {
      displayName: displayName("نام طراح"),
      maxLength: maxLength(50)
    },
    LookupId_AreaType: {
      displayName: displayName("حیطه سوال"),
      numeric,
      requiredDdl: requiredDdl(0)
    },
    ResponseSecond: {
      displayName: displayName("زمان پاسخ"),
      numeric,
      required
    },
    Description: {
      displayName: displayName("توضیحات"),
      maxLength: maxLength(300)
    }
  }
};

export { validationMixin, questionValidations };

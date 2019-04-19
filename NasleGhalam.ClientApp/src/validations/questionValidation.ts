import {
  required,
  displayName,
  maxLength,
  numeric
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
      required
    },
    QuestionPoint: {
      displayName: displayName("نمره"),
      numeric,
      required
    },
    LookupId_QuestionHardnessType: {
      displayName: displayName("درجه سختی"),
      numeric,
      required
    },
    AnswerNumber: {
      displayName: displayName("شماره گزینه صحیح"),
      numeric
    },
    LookupId_RepeatnessType: {
      displayName: displayName("درجه تکرار"),
      numeric,
      required
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
      required
    },
    AuthorName: {
      displayName: displayName("نام طراح"),
      maxLength: maxLength(50)
    },
    LookupId_AreaType: {
      displayName: displayName("حیطه سوال"),
      numeric,
      required
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

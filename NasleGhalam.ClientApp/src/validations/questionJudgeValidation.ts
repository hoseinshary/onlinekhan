import {
  required,
  displayName,
  numeric,
  requiredDdl
} from "src/plugins/vuelidate";
import { ValidationRuleset, validationMixin } from "vuelidate";
import IQuestionJudge from "src/models/IQuestionJudge";

type TQuestionJudge = {
  questionJudge: IQuestionJudge;
  validationGroup: string[];
};
const questionJudgeValidations: ValidationRuleset<TQuestionJudge> = {
  questionJudge: {
    IsStandard: {
      displayName: displayName("استاندارد"),
      required
    },
    IsDelete: {
      displayName: displayName("حذف"),
      required
    },
    IsUpdate: {
      displayName: displayName("ویرایش"),
      required
    },
    IsLearning: {
      displayName: displayName("یادگیری"),
      required
    },
    ResponseSecond: {
      displayName: displayName("مدت پاسخ"),
      numeric,
      required
    },
    LookupId_RepeatnessType: {
      displayName: displayName("درجه تکرار"),
      requiredDdl: requiredDdl(0)
    },
    LookupId_QuestionHardnessType: {
      displayName: displayName("درجه سختی"),
      requiredDdl: requiredDdl(0)
    }
  }
};

export { validationMixin, questionJudgeValidations };

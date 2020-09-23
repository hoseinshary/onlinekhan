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
    IsActiveQuestion: {
      displayName: displayName("تایید"),
      required
    },
    IsActiveQuestionAnswer: {
      displayName: displayName("تایید جواب"),
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
    },
    LookupId_WhereProblem: {
      displayName: displayName("مکان مشکل"),
      requiredDdl: requiredDdl(0)
    },
    LookupId_ReasonProblem: {
      displayName: displayName("علت مشکل"),
      requiredDdl: requiredDdl(0)
    },
    LookupId_QuestionRank: {
      displayName: displayName("امتیاز سوال"),
      requiredDdl: requiredDdl(0)
    },
    Description: {
      displayName: displayName("توضیحات")
    },
    EducationGroup: {
      displayName: displayName("گروه آموزشی")
    }
  }
};

export { validationMixin, questionJudgeValidations };

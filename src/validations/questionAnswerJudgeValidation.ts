import {
  required,
  displayName,
  requiredDdl,
  maxLength
} from "src/plugins/vuelidate";
import { ValidationRuleset, validationMixin } from "vuelidate";
import IQuestionAnswerJudge from "src/models/IQuestionAnswerJudge";

type TQuestionAnswerJudge = {
  questionAnswerJudge: IQuestionAnswerJudge;
  validationGroup: string[];
};
const questionAnswerJudgeValidations: ValidationRuleset<
  TQuestionAnswerJudge
> = {
  questionAnswerJudge: {
    IsActiveQuestionAnswer: {
      displayName: displayName("تایید"),
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
    QuestionAnswerId: {
      displayName: displayName("جواب سوال"),
      required
    },
    IsMaster: {
      displayName: displayName("آنلاین خوان"),
      required
    },
    LookupId_ReasonProblem: {
      displayName: displayName("دلیل مشکل"),
      requiredDdl: requiredDdl(0)
    },
    Description: {
      displayName: displayName("توضیحات"),
      maxLength: maxLength(400)
    }
  }
};

export { validationMixin, questionAnswerJudgeValidations };

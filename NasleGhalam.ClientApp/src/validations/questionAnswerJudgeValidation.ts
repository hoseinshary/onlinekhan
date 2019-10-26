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
      displayName: displayName("فعال"),
      required
    },
    QuestionAnswerId: {
      displayName: displayName("جواب سوال"),
      required
    },
    LessonName: {
      displayName: displayName("درس"),
      maxLength: maxLength(50),
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

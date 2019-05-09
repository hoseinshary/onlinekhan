import { displayName, maxLength, requiredDdl } from "src/plugins/vuelidate";
import { ValidationRuleset, validationMixin } from "vuelidate";
import IQuestionAnswer from "src/models/IQuestionAnswer";

type TQuestionAnswer = {
  questionAnswer: IQuestionAnswer;
  validationGroup: string[];
};
const questionAnswerValidations: ValidationRuleset<TQuestionAnswer> = {
  questionAnswer: {
    Title: {
      displayName: displayName("عنوان"),
      maxLength: maxLength(50)
      // required
    },
    Context: {
      displayName: displayName("متن")
    },
    LookupId_AnswerType: {
      displayName: displayName("نوع پاسخ")
    },
    Description: {
      displayName: displayName("توضیحات"),
      maxLength: maxLength(300)
    },
    Author: {
      displayName: displayName("نویسنده")
    },
    IsMaster: {
      displayName: displayName("آنلاین خوان")
    },
    QuestionId: {
      displayName: displayName("سوال"),
      requiredDdl: requiredDdl(0)
    }
  }
};

export { validationMixin, questionAnswerValidations };

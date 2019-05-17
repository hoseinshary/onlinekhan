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
    WriterId: {
      displayName: displayName("نویسنده"),
      requiredDdl: requiredDdl(0)
    },
    IsMaster: {
      displayName: displayName("آنلاین خوان")
    },
    IsActive: {
      displayName: displayName("فعال")
    },
    QuestionId: {
      displayName: displayName("سوال"),
      requiredDdl: requiredDdl(0)
    }
  }
};

export { validationMixin, questionAnswerValidations };

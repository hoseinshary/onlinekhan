import {
  required,
  displayName,
  maxLength,
  requiredDdl
} from "src/plugins/vuelidate";
import { ValidationRuleset, validationMixin } from "vuelidate";
import IQuestionGroup from "src/models/IQuestionGroup";

type TQuestionGroup = {
  questionGroup: IQuestionGroup;
  validationGroup: string[];
};
const questionGroupValidations: ValidationRuleset<TQuestionGroup> = {
  questionGroup: {
    Title: {
      displayName: displayName("عنوان"),
      maxLength: maxLength(50),
      required
    },
    LessonId: {
      displayName: displayName("درس"),
      requiredDdl: requiredDdl(0)
    }
  }
};

export { validationMixin, questionGroupValidations };

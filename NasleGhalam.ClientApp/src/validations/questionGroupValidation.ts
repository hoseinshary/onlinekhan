import { required, displayName, maxLength } from "src/plugins/vuelidate";
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
    }
  }
};

export { validationMixin, questionGroupValidations };

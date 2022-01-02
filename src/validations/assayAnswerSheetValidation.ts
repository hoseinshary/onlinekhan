import { required, displayName, maxLength } from "src/plugins/vuelidate";
import { ValidationRuleset, validationMixin } from "vuelidate";
import IAssayAnswerSheet from "src/models/IAssayAnswerSheet";

type TAssayAnswerSheet = { assayAnswerSheet: IAssayAnswerSheet; validationGroup: string[] };
const assayAnswerSheetValidations: ValidationRuleset<TAssayAnswerSheet> = {
  assayAnswerSheet: {
    AssayId: {
      displayName: displayName("آزمون"),
      required
    },
    AssayVarient: {
      displayName: displayName("نوع آزمون"),
      maxLength: maxLength(5),
      required
    }
  }
};

export { validationMixin, assayAnswerSheetValidations };

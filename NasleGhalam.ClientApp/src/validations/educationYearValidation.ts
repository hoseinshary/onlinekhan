import { required, displayName, maxLength } from "src/plugins/vuelidate";
import { ValidationRuleset, validationMixin } from "vuelidate";
import IEducationYear from "src/models/IEducationYear";

type TEducationYear = {
  educationYear: IEducationYear;
  validationGroup: string[];
};
const educationYearValidations: ValidationRuleset<TEducationYear> = {
  educationYear: {
    Name: {
      displayName: displayName("نام"),
      maxLength: maxLength(50),
      required
    },
    IsActiveYear: {
      displayName: displayName("سال جاری"),
      required
    }
  }
};

export { validationMixin, educationYearValidations };

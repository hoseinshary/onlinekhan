import {
  required,
  displayName,
  maxLength,
  requiredDdl
} from "src/plugins/vuelidate";
import { ValidationRuleset, validationMixin } from "vuelidate";
import IEducationSubGroup from "src/models/IEducationSubGroup";

type TEducationSubGroup = {
  educationSubGroup: IEducationSubGroup;
  validationGroup: string[];
};
const educationSubGroupValidations: ValidationRuleset<TEducationSubGroup> = {
  educationSubGroup: {
    Name: {
      displayName: displayName("نام"),
      maxLength: maxLength(50),
      required
    },
    EducationTreeId: {
      displayName: displayName("گروه آموزشی"),
      requiredDdl: requiredDdl(0)
    }
  }
};

export { validationMixin, educationSubGroupValidations };

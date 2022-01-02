import {
  required,
  displayName,
  maxLength,
  requiredDdl,
  numeric
} from "src/plugins/vuelidate";
import { ValidationRuleset, validationMixin } from "vuelidate";
import IProgram from "src/models/IProgram";

type TProgram = { program: IProgram; validationGroup: string[] };
const programValidations: ValidationRuleset<TProgram> = {
  program: {
    Name: {
      displayName: displayName("نام"),
      maxLength: maxLength(50),
      required
    },
    Description: {
      displayName: displayName("توضیحات"),
    },
    StudentId: {
      displayName: displayName("دانش آموز"),
      required
    }
  }
};

export { validationMixin, programValidations };

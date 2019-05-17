import { required, displayName, maxLength } from "src/plugins/vuelidate";
import { ValidationRuleset, validationMixin } from "vuelidate";
import IWriter from "src/models/IWriter";

type TWriter = { writer: IWriter; validationGroup: string[] };
const writerValidations: ValidationRuleset<TWriter> = {
  writer: {
    Name: {
      displayName: displayName("نام"),
      maxLength: maxLength(50),
      required
    }
  }
};

export { validationMixin, writerValidations };

import { required, displayName, maxLength } from "src/plugins/vuelidate";
import { ValidationRuleset, validationMixin } from "vuelidate";
import ITag from "src/models/ITag";

type TTag = { tag: ITag; validationGroup: string[] };
const tagValidations: ValidationRuleset<TTag> = {
  tag: {
    Name: {
      displayName: displayName("نام"),
      maxLength: maxLength(50),
      required
    },
    IsSource: {
      displayName: displayName("منبع"),
      required
    }
  }
};

export { validationMixin, tagValidations };

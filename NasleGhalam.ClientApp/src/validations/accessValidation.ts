import { displayName } from "src/plugins/vuelidate";
import { ValidationRuleset, validationMixin } from "vuelidate";
import IAccess from "src/models/IAccess";

type TAccess = { access: IAccess; validationGroup: string[] };
const accessValidations: ValidationRuleset<TAccess> = {
  access: {
    ModuleId: {
      displayName: displayName("منو")
    },
    ControllerId: {
      displayName: displayName("زیر منو")
    }
  }
};

export { validationMixin, accessValidations };

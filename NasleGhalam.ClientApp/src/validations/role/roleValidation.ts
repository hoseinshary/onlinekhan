import {
  required,
  displayName,
  maxLength,
  numeric
} from "src/plugins/vuelidate";
import { ValidationRuleset, validationMixin } from "vuelidate";
import IRole from "src/models/role/IRole";

type TRole = { role: IRole; validationGroup: string[] };
const roleValidations: ValidationRuleset<TRole> = {
  role: {
    Name: {
      displayName: displayName("نام"),
      maxLength: maxLength(50),
      required
    },
    Level: {
      displayName: displayName("سطح"),
      required,
      numeric
    },
    UserType: {
      displayName: displayName("نوع کاربری"),
      required
    }
  }
};

export { validationMixin, roleValidations };

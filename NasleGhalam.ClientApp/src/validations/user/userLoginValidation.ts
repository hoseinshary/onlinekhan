import { required, displayName } from "src/plugins/vuelidate";
import { ValidationRuleset, validationMixin } from "vuelidate";
import IUser from "src/models/IUser";

type TUser = { user: IUser; validationGroup: string[] };
const userLoginValidations: ValidationRuleset<TUser> = {
  user: {
    UserName: {
      displayName: displayName("نام کاربری"),
      required
    },
    Password: {
      displayName: displayName("رمز عبور"),
      required
    }
  }
};

export { validationMixin, userLoginValidations };

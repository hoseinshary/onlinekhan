import { required, displayName } from "src/plugins/vuelidate";
import { ValidationRuleset, validationMixin } from "vuelidate";
import ILogin from "src/models/ILogin";

type TLogin = { loginUser: ILogin; validationGroup: string[] };
const userLoginValidations: ValidationRuleset<TLogin> = {
  loginUser: {
    Username1: {
      displayName: displayName("نام کاربری"),
      required
    },
    Password1: {
      displayName: displayName("رمز عبور"),
      required
    }
  }
};

export { validationMixin, userLoginValidations };

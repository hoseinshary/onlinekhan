import { required, displayName } from "src/plugins/vuelidate";
import { ValidationRuleset, validationMixin } from "vuelidate";
import ILogin from "src/models/ILogin";

type TLogin = { loginUser: ILogin; validationGroup: string[] };
const userLoginValidations: ValidationRuleset<TLogin> = {
  loginUser: {
    Username: {
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

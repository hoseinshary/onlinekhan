import { required, displayName, maxLength , sameAs} from "src/plugins/vuelidate";
import { ValidationRuleset, validationMixin } from "vuelidate";
import IUserChangePassword from "src/models/IUserChangePassword";

type TUserChangePassword = { userChangePassword: IUserChangePassword; validationGroup: string[] };
const userChangePasswordValidations: ValidationRuleset<TUserChangePassword> = {
  userChangePassword: {
    OldPassword: {
      displayName: displayName("رمز عبور قدیم"),
      maxLength: maxLength(50),
      required
    },
    NewPassword: {
      displayName: displayName("رمز عبور جدید"),
      maxLength: maxLength(50),
      required
    },
    ReNewPassword: {
      displayName: displayName("تکرار رمز عبور جدید"),
      maxLength: maxLength(50),
      required
      //sameAsPassword: sameAs('NewPassword')
    },
  }
};

export { validationMixin, userChangePasswordValidations };

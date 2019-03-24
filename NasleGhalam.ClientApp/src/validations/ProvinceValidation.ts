import { required, displayName, maxLength } from "src/plugins/vuelidate";
import { ValidationRuleset, validationMixin } from "vuelidate";
import IProvince from "src/models/IProvince";

type TProvince = { province: IProvince; validationGroup: string[] };
const provinceValidations: ValidationRuleset<TProvince> = {
  province: {
    Name: {
      displayName: displayName("نام"),
      maxLength: maxLength(50),
      required
    },
    Code: {
      displayName: displayName("کد"),
      maxLength: maxLength(5),
      required
    }
  }
};

export { validationMixin, provinceValidations };

import {
  required,
  displayName,
  maxLength,
  requiredDdl
} from "src/plugins/vuelidate";
import { ValidationRuleset, validationMixin } from "vuelidate";
import ICity from "src/models/ICity";

type TCity = { city: ICity; validationGroup: string[] };
const cityValidations: ValidationRuleset<TCity> = {
  city: {
    Name: {
      displayName: displayName("نام"),
      maxLength: maxLength(50),
      required
    },
    ProvinceId: {
      displayName: displayName("استان"),
      requiredDdl: requiredDdl(0)
    }
  }
};

export { validationMixin, cityValidations };

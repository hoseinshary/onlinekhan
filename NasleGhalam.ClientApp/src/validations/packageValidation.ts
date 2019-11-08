import {
  required,
  displayName,
  maxLength,
  numeric
} from "src/plugins/vuelidate";
import { ValidationRuleset, validationMixin } from "vuelidate";
import IPackage from "src/models/IPackage";

type TPackage = { thePackage: IPackage; validationGroup: string[] };
const packageValidations: ValidationRuleset<TPackage> = {
  thePackage: {
    Name: {
      displayName: displayName("نام"),
      maxLength: maxLength(50),
      required
    },
    ImageFile: {
      displayName: displayName("تصویر")
    },
    IsActive: {
      displayName: displayName("فعال")
    },
    Price: {
      displayName: displayName("قیمت"),
      required,
      numeric
    },
    TimeDays: {
      displayName: displayName("مدت به روز"),
      required,
      numeric
    },
    Description: {
      displayName: displayName("توضیحات"),
      maxLength: maxLength(300)
    }
  }
};

export { validationMixin, packageValidations };

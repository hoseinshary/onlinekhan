import {
  required,
  displayName,
  maxLength,
  numeric
} from "src/plugins/vuelidate";
import { ValidationRuleset, validationMixin } from "vuelidate";
import IEducationBook from "src/models/IEducationBook";

type TEducationBook = {
  educationBook: IEducationBook;
  validationGroup: string[];
};
const educationBookValidations: ValidationRuleset<TEducationBook> = {
  educationBook: {
    Name: {
      displayName: displayName("نام کتاب"),
      maxLength: maxLength(50),
      required
    },
    PublishYear: {
      displayName: displayName("سال انتشار"),
      numeric,
      required
    },
    IsExamSource: {
      displayName: displayName("منبع کنکوری"),
      required
    },
    IsActive: {
      displayName: displayName("فعال"),
      required
    },
    IsChanged: {
      displayName: displayName("تغییر نسبت به سال قبل"),
      required
    }
  }
};

export { validationMixin, educationBookValidations };

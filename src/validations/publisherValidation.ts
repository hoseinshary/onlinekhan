import { required, displayName, maxLength } from "src/plugins/vuelidate";
import { ValidationRuleset, validationMixin } from "vuelidate";
import IPublisher from "src/models/IPublisher";

type TPublisher = { publisher: IPublisher; validationGroup: string[] };
const publisherValidations: ValidationRuleset<TPublisher> = {
  publisher: {
    Name: {
      displayName: displayName("نام"),
      maxLength: maxLength(50),
      required
    }
  }
};

export { validationMixin, publisherValidations };

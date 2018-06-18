import Vuelidate from "vuelidate";
import {
  minValue,
  required,
  minLength,
  maxLength
} from "vuelidate/lib/validators";
import vuelidateErrorExtractor, { templates } from "vuelidate-error-extractor";

export default ({ Vue }) => {
  Vue.use(Vuelidate);
  Vue.use(vuelidateErrorExtractor, {
    template: templates.foundation6,
    messages: {
      required: "{attribute} اجباری میباشد.",
      minLength: "{attribute} باید حداقل {min} کاراکتر باشد.",
      maxLength: "{attribute} باید حداکثر {max} کاراکتر باشد.",
      minValue: "{attribute} اجباری میباشد"
    }
  });
};

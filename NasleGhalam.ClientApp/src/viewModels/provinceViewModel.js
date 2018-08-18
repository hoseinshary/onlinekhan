import { required, displayName, maxLength } from 'plugins/vuelidate';

export default {
  provinceObj: {
    Name: {
      displayName: displayName('نام'),
      maxLength: maxLength(50),
      required
    },
    Code: {
      displayName: displayName('کد'),
      maxLength: maxLength(5),
      required
    }
  }
};

import { required, displayName, numeric, maxLength } from 'plugins/vuelidate';

export default {
  instanceObj: {
    Name: {
      displayName: displayName('نام'),
      maxLength: maxLength(50),
      required
    },
    Priority: {
      displayName: displayName('اولویت'),
      required,
      numeric
    }
  }
};

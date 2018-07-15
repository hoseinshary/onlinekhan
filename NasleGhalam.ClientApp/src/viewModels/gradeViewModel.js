import { required, displayName, numeric } from 'plugins/vuelidate';

export default {
  instanceObj: {
    Name: {
      displayName: displayName('نام'),
      required
    },
    Priority: {
      displayName: displayName('اولویت'),
      required,
      numeric
    }
  }
};

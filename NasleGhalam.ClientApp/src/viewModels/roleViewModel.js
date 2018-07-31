import { required, displayName, maxLength, numeric } from 'plugins/vuelidate';

export default {
  instanceObj: {
    Name: {
      displayName: displayName('نام'),
      maxLength: maxLength(50),
      required
    },
    Level: {
      displayName: displayName('سطح نقش'),
      required,
      numeric
    }
  }
};

import { required, displayName, maxLength, numeric } from 'plugins/vuelidate';

export default {
  roleObj: {
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

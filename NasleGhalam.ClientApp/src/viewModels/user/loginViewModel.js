import { required, displayName, maxLength } from 'plugins/vuelidate';

export default {
  instanceObj: {
    Username: {
      displayName: displayName('نام کاربری'),
      maxLength: maxLength(50),
      required
    },
    Password: {
      displayName: displayName('رمز عبور'),
      maxLength: maxLength(50),
      required
    }
  }
};

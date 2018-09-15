import { required, displayName } from 'plugins/vuelidate';

export default {
  loginObj: {
    UserName: {
      displayName: displayName('نام کاربری'),
      required
    },
    Password: {
      displayName: displayName('رمز عبور'),
      required
    }
  }
};

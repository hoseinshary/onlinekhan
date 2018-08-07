import { required, displayName, numeric, maxLength } from 'plugins/vuelidate';

export default {
  gradeObj: {
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

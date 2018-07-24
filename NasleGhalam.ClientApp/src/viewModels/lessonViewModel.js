import {
  required,
  displayName,
  maxLength
} from 'plugins/vuelidate';

export default {
  instanceObj: {
    Name: {
      displayName: displayName('نام'),
      maxLength: maxLength(50),
      required
    }
  }
};

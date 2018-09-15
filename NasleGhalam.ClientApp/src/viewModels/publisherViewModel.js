import {
  required,
  displayName,
  maxLength,
  numeric,
  requiredDdl
} from 'plugins/vuelidate';

export default {
  publisherObj: {
    Name: {
      displayName: displayName('نام'),
      maxLength: maxLength(50),
      required
    }
  }
};

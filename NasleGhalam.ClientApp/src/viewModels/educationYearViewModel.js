import {
  required,
  displayName,
  maxLength,
  numeric,
  requiredDdl
} from 'plugins/vuelidate';

export default {
  educationYearObj: {
    Name: {
      displayName: displayName('نام'),
      maxLength: maxLength(50),
      required
    },
    IsActiveYear: {
      displayName: displayName('سال جاری'),
      required
    }
  }
};

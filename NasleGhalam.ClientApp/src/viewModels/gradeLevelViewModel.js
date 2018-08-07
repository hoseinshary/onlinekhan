import {
  required,
  displayName,
  numeric,
  maxLength,
  requiredDdl
} from 'plugins/vuelidate';

export default {
  gradeLevelObj: {
    Name: {
      displayName: displayName('نام'),
      maxLength: maxLength(50),
      required
    },
    Priority: {
      displayName: displayName('اولویت'),
      required,
      numeric
    },
    GradeId: {
      displayName: displayName('مقطع تحصیلی'),
      requiredDdl: requiredDdl(0)
    }
  }
};

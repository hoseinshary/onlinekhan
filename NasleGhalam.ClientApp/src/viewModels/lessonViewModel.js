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
    },
    IsMain: {
      displayName: displayName('اختصاصی'),
      required
    },
    LookupId_Nezam: {
      displayName: displayName('نظام'),
      required
    },
    GradeId: {
      displayName: displayName('مقطع')
      // required
    },
    GradeLevelId: {
      displayName: displayName('پایه')
      // required
    },
    TreeId_Grade: {
      displayName: displayName('مقطع')
    }
  }
};

import {
  required,
  displayName,
  maxLength,
  numeric,
  requiredDdl
} from 'plugins/vuelidate';

export default {
  examObj: {
    Name: {
      displayName: displayName('نام'),
      maxLength: maxLength(50),
      required
    },
    Date: {
      displayName: displayName('تاریخ امتحان'),
      maxLength: maxLength(50),
      required
    },
    EducationGroupName: {
      displayName: displayName('گروه آموزشی')
    },
    EducationGroupId: {
      displayName: displayName('گروه آموزشی'),
      numeric,
      required
    },
    EducationYearName: {
      displayName: displayName('سال تحصیلی')
    },
    EducationYearId: {
      displayName: displayName('سال تحصیلی'),
      numeric,
      required
    }
  }
};

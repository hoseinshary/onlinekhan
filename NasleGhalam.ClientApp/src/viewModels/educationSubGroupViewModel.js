import {
  required,
  displayName,
  maxLength,
  requiredDdl
} from 'plugins/vuelidate';

export default {
  educationSubGroupObj: {
    Name: {
      displayName: displayName('نام'),
      maxLength: maxLength(50),
      required
    },
    EducationGroupId: {
      displayName: displayName('گروه آموزشی'),
      requiredDdl: requiredDdl(0)
    }
  }
};

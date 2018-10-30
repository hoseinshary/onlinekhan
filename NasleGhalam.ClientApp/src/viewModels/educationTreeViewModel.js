import {
  required,
  displayName,
  maxLength,
  numeric,
  requiredDdl
} from 'plugins/vuelidate';

export default {
  educationTreeObj: {
    Name: {
      displayName: displayName('نام'),
      maxLength: maxLength(50),
      required
    },
    LookupId_EducationTreeState: {
      displayName: displayName('نوع درخت آموزش'),
      numeric,
      required
    },
    ParentEducationTreeId: {
      displayName: displayName('درخت آموزش پدر'),
      numeric
      // required
    }
  }
};

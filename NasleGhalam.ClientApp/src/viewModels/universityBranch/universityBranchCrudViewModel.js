import {
  required,
  displayName,
  maxLength,
  numeric,
  requiredDdl
} from 'plugins/vuelidate';

export default {
  universityBranchObj: {
    Name: {
      displayName: displayName('نام'),
      maxLength: maxLength(50),
      required
    },
    Balance: {
      displayName: displayName('تراز'),
      numeric,
      required
    },
    EducationSubGroupId: {
      displayName: displayName('زیر گروه آموزشی'),
      requiredDdl: requiredDdl(0)
    }
  }
};

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
    SiteAverage: {
      displayName: displayName('معدل سایت'),
      numeric
    },
    Balance1Low: {
      displayName: displayName('کمینه تراز 1'),
      numeric
    },
    Balance1High: {
      displayName: displayName('بیشینه تراز 1'),
      numeric
    },
    Balance2Low: {
      displayName: displayName('کمینه تراز 2'),
      numeric
    },
    Balance2High: {
      displayName: displayName('بیشینه تراز 2'),
      numeric
    },
    EducationSubGroupId: {
      displayName: displayName('زیر گروه آموزشی'),
      requiredDdl: requiredDdl(0)
    }
  }
};

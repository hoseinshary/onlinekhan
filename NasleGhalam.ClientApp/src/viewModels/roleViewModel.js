import {
  required,
  displayName,
  maxLength,
  numeric,
  requiredDdl
} from 'plugins/vuelidate';

export default {
  instanceObj: {
    Name: {
      displayName: displayName('Name'),
      maxLength: maxLength(50),
      required
    },
    Priority: {
      displayName: displayName('Priority'),

      required
    },
    GradeId: {
      displayName: displayName('GradeId'),

      requiredDdl: requiredDdl(0)
    }
  }
};

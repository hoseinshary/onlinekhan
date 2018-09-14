import { displayName, requiredDdl } from 'plugins/vuelidate';

export default {
  educationBookIndexObj: {
    GradeId: {
      displayName: displayName('مقطع تحصیلی'),
      requiredDdl: requiredDdl(0)
    },
    GradeLevelId: {
      displayName: displayName('پایه تحصیلی'),
      requiredDdl: requiredDdl(0)
    }
  }
};

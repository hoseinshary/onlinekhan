import { displayName, requiredDdl } from 'plugins/vuelidate';

export default {
  educationBookIndexObj: {
    EducationTreeId_Grade: {
      displayName: displayName('مقطع تحصیلی'),
      requiredDdl: requiredDdl(0)
    },
    LessonId: {
      displayName: displayName('درس'),
      requiredDdl: requiredDdl(0)
    }
  }
};

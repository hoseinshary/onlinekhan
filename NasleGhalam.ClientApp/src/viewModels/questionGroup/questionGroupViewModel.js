import {
  required,
  displayName,
  maxLength,
  numeric,
  requiredDdl
} from 'plugins/vuelidate';

export default {
  questionGroupObj: {
    Title: {
      displayName: displayName('عنوان'),
      maxLength: maxLength(50),
      required
    },
    LessonId: {
      displayName: displayName('LessonId'),
      maxLength: maxLength(50),
      required
    }
  }
};

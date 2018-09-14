import {
  required,
  displayName,
  maxLength,
  numeric,
  requiredDdl
} from 'plugins/vuelidate';

export default {
  educationBookObj: {
    Name: {
      displayName: displayName('نام کتاب'),
      maxLength: maxLength(50),
      required
    },
    PublishYear: {
      displayName: displayName('سال انتشار'),
      numeric,
      required
    },
    IsExamSource: {
      displayName: displayName('منبع کنکوری'),
      required
    },
    IsActive: {
      displayName: displayName('فعال'),
      required
    },
    IsChanged: {
      displayName: displayName('تغییر نسبت به سال قبل'),
      required
    },
    EducationGroup_LessonId: {
      displayName: displayName('درس'),
      requiredDdl: requiredDdl(0)
    },
    EducationGroupId: {
      displayName: displayName('گروه آموزشی'),
      requiredDdl: requiredDdl(0)
    }
  }
};

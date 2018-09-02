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
      displayName: displayName('نام'),
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
    GradeId: {
      displayName: displayName('مقطع تحصیلی'),
      requiredDdl: requiredDdl(0)
    },
    GradeLevelId: {
      displayName: displayName('پایه تحصیلی'),
      requiredDdl: requiredDdl(0)
    },
    EducationGroup_LessonId: {
      displayName: displayName('درس'),
      requiredDdl: requiredDdl(0)
    }
  }
};

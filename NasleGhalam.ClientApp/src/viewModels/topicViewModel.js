import {
  required,
  displayName,
  maxLength,
  numeric,
  requiredDdl
} from 'plugins/vuelidate';

export default {
  topicObj: {
    Title: {
      displayName: displayName('عنوان'),
      maxLength: maxLength(50),
      required
    },
    ExamStock: {
      displayName: displayName('سهمیه در کنکور'),
      numeric,
      required
    },
    ExamStockSystem: {
      displayName: displayName('سهمیه در کنکور سیستمی')
      // numeric,
      // required
    },
    Importance: {
      displayName: displayName('ضریب اهمیت'),
      numeric,
      required
    },
    IsExamSource: {
      displayName: displayName('مبحث کنکوری'),

      required
    },
    LookupId_HardnessType: {
      displayName: displayName('درجه سختی'),
      numeric,
      required
    },
    LookupId_AreaType: {
      displayName: displayName('حیطه مبحث'),
      numeric,
      required
    },
    IsActive: {
      displayName: displayName('فعال'),

      required
    },
    ParentTopicId: {
      displayName: displayName('مبحث پدر')
      // numeric,
      // required
    },
    EducationGroup_LessonId: {
      displayName: displayName('درس')
      // numeric,
      // required
    },
    EducationGroupId: {
      displayName: displayName('گروه آموزشی')
      // numeric,
      // required
    },
    LookupId_Nezam: {
      displayName: displayName('نظانم (اختیاری)')
    },
    GradeId: {
      displayName: displayName('مقطع (اختیاری)')
    },
    GradeLevelId: {
      displayName: displayName('پایه (اختیاری)')
    }
  }
};

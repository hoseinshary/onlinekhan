import {
  required,
  displayName,
  maxLength,
  numeric,
  requiredDdl
} from 'plugins/vuelidate';

export default {
  questionObj: {
    Context: {
      displayName: displayName('متن'),
      maxLength: maxLength(50),
      required
    },
    QuestionNumber: {
      displayName: displayName('شماره سوال'),
      numeric,
      required
    },
    LookupId_QuestionType: {
      displayName: displayName('نوع سوال'),
      numeric,
      required
    },
    QuestionPoint: {
      displayName: displayName('نمره'),
      numeric,
      required
    },
    LookupId_QuestionHardnessType: {
      displayName: displayName('درجه سختی'),
      numeric,
      required
    },
    AnswerNumber: {
      displayName: displayName('شماره گزینه صحیح'),
      numeric,
      required
    },
    LookupId_RepeatnessType: {
      displayName: displayName('درجه تکرار'),
      numeric,
      required
    },
    UseEvaluation: {
      displayName: displayName('ارزیابی'),

      required
    },
    IsStandard: {
      displayName: displayName('استاندارد'),

      required
    },
    LookupId_AuthorType: {
      displayName: displayName('نوع طراح'),
      numeric,
      required
    },
    AuthorName: {
      displayName: displayName('نام طراح'),
      maxLength: maxLength(50),
      required
    },
    LookupId_AreaType: {
      displayName: displayName('حیطه سوال'),
      numeric,
      required
    },
    ResponseSecond: {
      displayName: displayName('زمان پاسخ'),
      numeric,
      required
    },
    Description: {
      displayName: displayName('توضیحات'),
      maxLength: maxLength(50),
      required
    },
    FileName: {
      displayName: displayName('نام فایل'),
      maxLength: maxLength(50),
      required
    },
    InsertDateTime: {
      displayName: displayName('تاریخ ورود داده'),
      maxLength: maxLength(50),
      required
    },
    UserId: {
      displayName: displayName('کاربر'),
      numeric,
      required
    },
    File: {
      displayName: displayName('فایل سوال')
    },
    EducationGroupId: {
      displayName: displayName('گروه تحصیلی')
    },
    EducationTreeId_Grade: {
      displayName: displayName('مقطع')
    },
    EducationGroup_LessonId: {
      displayName: displayName('درس')
    },
    EducationTreeIds: {
      displayName: displayName('درخت آموزش')
    },
    LessonId: {
      displayName: displayName('درس')
    },
    TopicIds: {
      displayName: displayName('گره والد')
    }
  }
};

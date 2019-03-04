import { required, displayName, numeric, requiredDdl } from 'plugins/vuelidate';

export default {
  questionJudgeObj: {
    IsStandard: {
      displayName: displayName('استاندارد'),
      required
    },
    IsDelete: {
      displayName: displayName('حذف'),
      required
    },
    IsUpdate: {
      displayName: displayName('ویرایش'),
      required
    },
    IsLearning: {
      displayName: displayName('یادگیری'),
      required
    },
    ResponseSecond: {
      displayName: displayName('مدت پاسخ'),
      numeric,
      required
    },
    LookupId_RepeatnessType: {
      displayName: displayName('درجه تکرار'),
      requiredDdl: requiredDdl(0)
    },
    LookupId_QuestionHardnessType: {
      displayName: displayName('درجه سختی'),
      requiredDdl: requiredDdl(0)
    }
  }
};

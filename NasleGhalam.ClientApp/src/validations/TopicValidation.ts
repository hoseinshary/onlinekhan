import {
  required,
  displayName,
  maxLength,
  numeric,
  requiredDdl
} from "src/plugins/vuelidate";
import { ValidationRuleset, validationMixin } from "vuelidate";
import ITopic from "src/models/ITopic";

type TTopic = { topic: ITopic; validationGroup: string[] };
const topicValidations: ValidationRuleset<TTopic> = {
  topic: {
    Title: {
      displayName: displayName("عنوان"),
      maxLength: maxLength(200),
      required
    },
    ExamStock: {
      displayName: displayName("سهمیه در کنکور"),
      numeric,
      required
    },
    Importance: {
      displayName: displayName("ضریب اهمیت"),
      numeric,
      required
    },
    IsExamSource: {
      displayName: displayName("مبحث کنکوری"),
      required
    },
    LookupId_HardnessType: {
      displayName: displayName("درجه سختی"),
      requiredDdl: requiredDdl(0)
    },
    LookupId_AreaType: {
      displayName: displayName("حیطه مبحث"),
      requiredDdl: requiredDdl(0)
    },
    DisplayPriority: {
      displayName: displayName("اولویت نمایش"),
      numeric,
      required
    },
    IsActive: {
      displayName: displayName("فعال"),
      required
    },
    ParentTopicId: {
      displayName: displayName("مبحث پدر")
    },
    LessonId: {
      displayName: displayName("درس"),
      requiredDdl: requiredDdl(0)
    }
  }
};

export { validationMixin, topicValidations };

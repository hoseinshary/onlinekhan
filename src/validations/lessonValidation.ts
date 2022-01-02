import {
  required,
  displayName,
  maxLength,
  requiredDdl,
  numeric
} from "src/plugins/vuelidate";
import { ValidationRuleset, validationMixin } from "vuelidate";
import ILesson from "src/models/ILesson";

type TLesson = { lesson: ILesson; validationGroup: string[] };
const lessonValidations: ValidationRuleset<TLesson> = {
  lesson: {
    Name: {
      displayName: displayName("نام"),
      maxLength: maxLength(50),
      required
    },
    IsMain: {
      displayName: displayName("اختصاصی"),
      required
    },
    LookupId_Nezam: {
      displayName: displayName("نظام"),
      requiredDdl: requiredDdl(0)
    },
    NumberOfJudges: {
      displayName: displayName("تعداد ارزیابی"),
      required,
      numeric
    },
    LessonDepartmentId: {
      displayName: displayName("بخش")
    }
  }
};

export { validationMixin, lessonValidations };

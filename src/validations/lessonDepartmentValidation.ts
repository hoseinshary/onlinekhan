import { required, displayName, maxLength } from "src/plugins/vuelidate";
import { ValidationRuleset, validationMixin } from "vuelidate";
import ILessonDepartment from "src/models/ILessonDepartment";

type TLessonDepartment = {
  lessonDepartment: ILessonDepartment;
  validationGroup: string[];
};
const lessonDepartmentValidations: ValidationRuleset<TLessonDepartment> = {
  lessonDepartment: {
    Name: {
      displayName: displayName("نام"),
      maxLength: maxLength(50),
      required
    }
  }
};

export { validationMixin, lessonDepartmentValidations };

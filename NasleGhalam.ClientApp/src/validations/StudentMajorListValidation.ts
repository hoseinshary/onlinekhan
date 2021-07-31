import { required, displayName, maxLength } from "src/plugins/vuelidate";
import { ValidationRuleset, validationMixin } from "vuelidate";
import IStudentMajorList from "src/models/IStudentMajorList";

type TStudentMajorList = { studentMajorList: IStudentMajorList; validationGroup: string[] };
const studentMajorListValidations: ValidationRuleset<TStudentMajorList> = {
  studentMajorList: {
    Title: {
      displayName: displayName("نام"),
      maxLength: maxLength(50),
      required
    },
    Code: {
      displayName: displayName("کد"),
      maxLength: maxLength(5),
      required
    }
  }
};

export { validationMixin, studentMajorListValidations };

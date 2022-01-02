import { required, displayName, maxLength } from "src/plugins/vuelidate";
import { ValidationRuleset, validationMixin } from "vuelidate";
import IStudentMajorList from "src/models/IStudentMajorList";

type TStudentMajorList = { studentMajorList: IStudentMajorList; validationGroup: string[] };
const studentMajorListValidations: ValidationRuleset<TStudentMajorList> = {
  studentMajorList: {
    Title: {
      displayName: displayName("نام لیست"),
      maxLength: maxLength(50),
      required
    }
  }
};

export { validationMixin, studentMajorListValidations };

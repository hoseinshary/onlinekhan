import { required, displayName, maxLength } from "src/plugins/vuelidate";
import { ValidationRuleset, validationMixin } from "vuelidate";
import ITeacherGroup from "src/models/ITeacherGroup";
type TteacherGroup = { teacherGroup: ITeacherGroup; validationGroup: string[] };
const teacherGroupValidations: ValidationRuleset<TteacherGroup> = {
  teacherGroup: {
    Name: {
      displayName: displayName("نام"),
      maxLength: maxLength(50),
      required
    },
    Id: {
      displayName: displayName("کد"),
      maxLength: maxLength(5),
      required
    },
    TeacherId: {
      displayName: displayName("کد استاد"),
      maxLength: maxLength(10),
    },
    StudentsId:{
      displayName: displayName("اعضای گروه"),
      maxLength: maxLength(50),
    }

  }
};

export { validationMixin, teacherGroupValidations };

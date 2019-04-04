import IEducationSubGroup from "./IEducationSubGroup";
import IEducationTree from "./IEducationTree";
import ILesson from "./ILesson";

export default interface IRatio {
  Id: number;
  Rate: number;
  LessonId: number;
  Lesson?: ILesson;
  EducationSubGroupId: number;
  EducationSubGroup?: IEducationSubGroup;
  EducationTreeId: number;
  EducationTree?: IEducationTree;
}

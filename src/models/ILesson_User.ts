export default interface ILesson_User {
  UserIds: Array<number>;
  LessonIds: Array<number>;
}

export const DefaultLesson_User: ILesson_User = {
  UserIds: [],
  LessonIds: []
};

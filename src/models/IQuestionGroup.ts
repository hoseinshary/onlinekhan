export default interface IQuestionGroup {
  Id: number;
  Title: string;
  PInsertTime: string;
  LessonId: number;
  QuestionGroupWordPath: string;
  QuestionGroupExcelPath: string;
}

export const DefaultQuestionGroup: IQuestionGroup = {
  Id: 0,
  Title: "",
  PInsertTime: "",
  LessonId: 0,
  QuestionGroupExcelPath: "",
  QuestionGroupWordPath: ""
};

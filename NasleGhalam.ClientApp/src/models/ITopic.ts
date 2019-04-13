export default interface ITopic {
  Id: number;
  Title: string;
  ExamStock: number;
  ExamStockSystem: number;
  Importance: number | string;
  IsExamSource: boolean;
  IsActive: boolean;
  LookupId_HardnessType: number;
  LookupId_AreaType: number;
  LessonId: number;
  ParentTopicId?: number;
}

export const DefaultTopic: ITopic = {
  Id: 0,
  Title: "",
  ExamStock: 0,
  ExamStockSystem: 0,
  Importance: "",
  IsExamSource: false,
  IsActive: true,
  LookupId_HardnessType: 0,
  LookupId_AreaType: 0,
  LessonId: 0
};

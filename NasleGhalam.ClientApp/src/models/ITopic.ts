export default interface ITopic {
  Id: number;
  Title: string;
  ExamStock: number;
  ExamStockSystem: number;
  Importance: number;
  IsExamSource: boolean;
  IsActive: boolean;
  LookupId_HardnessType: number;
  LookupId_AreaType: number;
  LessonId: number;
  ParentTopicId?: number;
  ParentTopic?: ITopic;
}

export const DefaultTopic: ITopic = {
  Id: 0,
  Title: "",
  ExamStock: 0,
  ExamStockSystem: 0,
  Importance: 0,
  IsExamSource: false,
  IsActive: true,
  LookupId_HardnessType: 0,
  LookupId_AreaType: 0,
  LessonId: 0
};

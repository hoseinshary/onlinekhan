export default interface ITopic {
  Id: number;
  Title: string;
  ExamStock: number;
  ExamStockSystem: number;
  Importance: number;
  IsExamSource: boolean;
  IsActive: boolean;
  DisplayPriority: number;
  LookupId_HardnessType: number;
  LookupId_AreaType: number;
  LessonId: number;
  ParentTopicId?: number | null;
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
  DisplayPriority: 0,
  LookupId_HardnessType: 0,
  LookupId_AreaType: 0,
  LessonId: 0,
  ParentTopicId: null
};

import IQuestionOption from "./IQuestionOption";
import ITopic from "./ITopic";

export default interface IQuestion {
  Id: number;
  Context: string;
  QuestionNumber: number;
  QuestionPoint: number;
  UseEvaluation: boolean;
  IsStandard: boolean;
  AuthorName: string;
  ResponseSecond: number;
  Description: string;
  FileName: string;
  // IsActive: boolean;
  AnswerNumber: number;
  QuestionWordPath: string;
  QuestionPicturePath: string;
  LookupId_QuestionType: number;
  LookupId_QuestionHardnessType: number;
  LookupId_RepeatnessType: number;
  LookupId_AuthorType: number;
  LookupId_AreaType: number;
  QuestionOptions?: Array<IQuestionOption>;
  Topics?: Array<ITopic>;
  Tags?: Array<ITopic>;
  TopicIds?: Array<number>;
  TagIds?: Array<number>;
}

export const DefaultQuestion: IQuestion = {
  Id: 0,
  Context: "",
  QuestionNumber: 0,
  QuestionPoint: 0,
  UseEvaluation: false,
  IsStandard: false,
  AuthorName: "",
  ResponseSecond: 0,
  Description: "",
  FileName: "",
  // IsActive: true,
  AnswerNumber: 0,
  QuestionWordPath: "",
  QuestionPicturePath: "",
  LookupId_QuestionType: 6,
  LookupId_QuestionHardnessType: 12,
  LookupId_RepeatnessType: 22,
  LookupId_AuthorType: 1039,
  LookupId_AreaType: 1036,
  QuestionOptions: [],
  Topics: [],
  Tags: [],
  TopicIds: [],
  TagIds: []
};

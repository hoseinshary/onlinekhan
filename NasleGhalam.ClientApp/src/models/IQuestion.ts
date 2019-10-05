import IQuestionOption from "./IQuestionOption";
import ITopic from "./ITopic";
import IQuestionAnswer from "./IQuestionAnswer";
import ILookup, { DefaultLookup } from "./ILookup";
import IWriter, { DefaultWriter } from "./IWriter";

export default interface IQuestion {
  Id: number;
  Context: string;
  QuestionNumber: number;
  QuestionPoint: number;
  UseEvaluation: boolean;
  IsStandard: boolean;
  WriterId: number;
  ResponseSecond: number;
  Description: string;
  FileName: string;
  // IsActive: boolean;
  IsHybrid : boolean;
  AnswerNumber: number;
  TopicAnswer : string;
  QuestionWordPath: string;
  QuestionPicturePath: string;
  LookupId_QuestionType: number;
  LookupId_QuestionHardnessType: number;
  LookupId_RepeatnessType: number;
  LookupId_AuthorType: number;
  LookupId_AreaType: number;
  LookupId_QuestionRank: number;
  Lookup_AreaType?: ILookup;
  Writer?: IWriter;
  QuestionOptions?: Array<IQuestionOption>;
  QuestionAnswers?: Array<IQuestionAnswer>;
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
  WriterId: 0,
  ResponseSecond: 0,
  Description: "",
  FileName: "",
  // IsActive: true,
  IsHybrid : false,
  AnswerNumber: 0,
  TopicAnswer : "",
  QuestionWordPath: "",
  QuestionPicturePath: "",
  LookupId_QuestionType: 6,
  LookupId_QuestionHardnessType: 12,
  LookupId_RepeatnessType: 22,
  LookupId_AuthorType: 1039,
  LookupId_AreaType: 1036,
  LookupId_QuestionRank: 1063,
  Lookup_AreaType: DefaultLookup,
  Writer: DefaultWriter,
  QuestionOptions: [],
  QuestionAnswers: [],
  Topics: [],
  Tags: [],
  TopicIds: [],
  TagIds: []
};

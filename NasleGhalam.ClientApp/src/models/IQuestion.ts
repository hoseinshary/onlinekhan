import IQuestionOption from "./IQuestionOption";
import ITopic from "./ITopic";
import IQuestionAnswer from "./IQuestionAnswer";
import ILookup, { DefaultLookup } from "./ILookup";
import IWriter, { DefaultWriter } from "./IWriter";
import IUser, { DefaultUser } from "./IUser";

export default interface IQuestion {
  Id: number;
  Context: string;
  QuestionNumber: number;
  QuestionPoint: number;
  UseEvaluation: boolean;
  IsStandard: boolean;
  WriterId: number;
  SupervisorUserId : number;
  ResponseSecond: number;
  Description: string;
  FileName: string;
  IsActive: boolean;
  IsHybrid : boolean;
  AnswerNumber: number;
  TopicAnswer  : Array<string>;
  QuestionWordPath: string;
  QuestionPicturePath: string;
  LookupId_QuestionType: number;
  LookupId_QuestionHardnessType: number;
  LookupId_RepeatnessType: number;
  LookupId_AuthorType: number;
  LookupId_AreaType: Array<number>;
  LookupId_QuestionRank: number;
  Lookup_AreaTypes?: Array<ILookup>;
  Writer?: IWriter;
  SupervisorUser?: IUser;
  QuestionOptions?: Array<IQuestionOption>;
  QuestionAnswers?: Array<IQuestionAnswer>;
  Topics?: Array<ITopic>;
  Tags?: Array<ITopic>;
  TopicIds?: Array<number>;
  TagIds?: Array<number>;
  IsDelete : boolean;
}

export const DefaultQuestion: IQuestion = {
  Id: 0,
  Context: "",
  QuestionNumber: 0,
  QuestionPoint: 0,
  UseEvaluation: false,
  IsStandard: false,
  WriterId: 0,
  SupervisorUserId: 0,
  ResponseSecond: 0,
  Description: "",
  FileName: "",
  IsActive: false,
  IsHybrid : false,
  AnswerNumber: 0,
  TopicAnswer : [],
  QuestionWordPath: "",
  QuestionPicturePath: "",
  LookupId_QuestionType: 6,
  LookupId_QuestionHardnessType: 12,
  LookupId_RepeatnessType: 22,
  LookupId_AuthorType: 1039,
  LookupId_AreaType: [],
  LookupId_QuestionRank: 1063,
  Lookup_AreaTypes: [],
  Writer: DefaultWriter,
  SupervisorUser : DefaultUser,
  QuestionOptions: [],
  QuestionAnswers: [],
  Topics: [],
  Tags: [],
  TopicIds: [],
  TagIds: [],
  IsDelete : false,

};

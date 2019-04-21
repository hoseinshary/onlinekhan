import ILookup from "./ILookup";

export default interface IQuestionJudge {
  Id: number;
  IsStandard: boolean;
  IsDelete: boolean;
  IsUpdate: boolean;
  IsLearning: boolean;
  ResponseSecond: number;
  QuestionId: number;
  LookupId_QuestionHardnessType: number;
  LookupId_RepeatnessType: number;
  Lookup_QuestionHardnessType?: ILookup;
  Lookup_RepeatnessType?: ILookup;
}

export const DefaultQuestionJudge: IQuestionJudge = {
  Id: 0,
  IsStandard: false,
  IsDelete: false,
  IsUpdate: false,
  IsLearning: false,
  ResponseSecond: 0,
  QuestionId: 0,
  LookupId_QuestionHardnessType: 0,
  LookupId_RepeatnessType: 0
};

import ILookup from "./ILookup";

export default interface IQuestionJudge {
  Id: number;
  IsStandard: boolean;
  IsDelete: boolean;
  IsUpdate: boolean;
  IsLearning: boolean;
  IsActiveQuestion: boolean;
  IsActiveQuestionAnswer: boolean;
  ResponseSecond: number;
  QuestionId: number;
  LookupId_QuestionHardnessType: number;
  LookupId_RepeatnessType: number;
  LookupId_WhereProblem: number;
  LookupId_ReasonProblem: number;
  Lookup_QuestionHardnessType?: ILookup;
  Lookup_RepeatnessType?: ILookup;
  Lookup_WhereProblem?: ILookup;
  Lookup_ReasonProblem?: ILookup;
  Description: string;
  
}

export const DefaultQuestionJudge: IQuestionJudge = {
  Id: 0,
  IsStandard: false,
  IsDelete: false,
  IsUpdate: false,
  IsLearning: false,
  IsActiveQuestion: false,
  IsActiveQuestionAnswer: false,
  ResponseSecond: 0,
  QuestionId: 0,
  LookupId_QuestionHardnessType: 0,
  LookupId_RepeatnessType: 0,
  LookupId_WhereProblem: 0,
  LookupId_ReasonProblem: 0,
  Description: ""
};

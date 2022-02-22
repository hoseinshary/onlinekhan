import ILookup from "./ILookup";
import IUser from "./IUser";

export default interface IQuestionJudge {
  Id: number;
  IsStandard: boolean;
  IsDelete: boolean;
  IsUpdate: boolean;
  IsLearning: boolean;
  IsActiveQuestion: boolean;
  IsActiveQuestionAnswer: boolean;
  ResponseSecond: number | string;
  QuestionId: number;
  LookupId_QuestionHardnessType: number;
  LookupId_RepeatnessType: number;
  LookupId_WhereProblem: number;
  LookupId_ReasonProblem: number;
  LookupId_QuestionRank: number;
  Lookup_QuestionHardnessType?: ILookup;
  Lookup_RepeatnessType?: ILookup;
  Lookup_WhereProblem?: ILookup;
  Lookup_ReasonProblem?: ILookup;
  Lookup_QuestionRank?: ILookup;
  EducationGroup: string;
  Description: string;
  UserId : number;
  User ?: IUser;
  
}

export const DefaultQuestionJudge: IQuestionJudge = {
  Id: 0,
  IsStandard: true,
  IsDelete: false,
  IsUpdate: false,
  IsLearning: false,
  IsActiveQuestion: false,
  IsActiveQuestionAnswer: false,
  ResponseSecond: "",
  QuestionId: 0,
  LookupId_QuestionHardnessType: 0,
  LookupId_RepeatnessType: 0,
  LookupId_WhereProblem: 1066,
  LookupId_ReasonProblem: 1067,
  LookupId_QuestionRank: 0,
  Description: "",
  EducationGroup: "",
  UserId :0
};

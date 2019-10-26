import ILookup from "./ILookup";
import IUser from "./IUser";

export default interface IQuestionAnswerJudge {
  Id: number;
  IsActiveQuestionAnswer: boolean;
  QuestionAnswerId: number;
  LessonName: string;
  LookupId_ReasonProblem: number;
  Description: string;
  Lookup_ReasonProblem?: ILookup;
  User?: IUser;
}

export const DefaultQuestionAnswerJudge: IQuestionAnswerJudge = {
  Id: 0,
  IsActiveQuestionAnswer: false,
  QuestionAnswerId: 0,
  LessonName: "",
  LookupId_ReasonProblem: 0,
  Description: ""
};

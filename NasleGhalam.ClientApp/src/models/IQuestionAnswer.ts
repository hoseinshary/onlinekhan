import IWriter from "./IWriter";

export default interface IQuestionAnswer {
  Id: number;
  Title: string;
  Context: string;
  LookupId_AnswerType: number;
  Description: string;
  WriterId: number;
  IsMaster: boolean;
  IsActive: boolean;
  QuestionId: number;
  Writer?: IWriter;
}

export const DefaultQuestionAnswer: IQuestionAnswer = {
  Id: 0,
  Title: "",
  Context: "",
  LookupId_AnswerType: 0,
  Description: "",
  WriterId: 0,
  IsMaster: false,
  IsActive: false,
  QuestionId: 0
};

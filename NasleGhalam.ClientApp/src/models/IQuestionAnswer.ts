export default interface IQuestionAnswer {
  Id: number;
  Title: string;
  Context: string;
  LookupId_AnswerType: number;
  Description: string;
  Author: string;
  IsMaster: boolean;
  IsActive: boolean;
  QuestionId: number;
}

export const DefaultQuestionAnswer: IQuestionAnswer = {
  Id: 0,
  Title: "",
  Context: "",
  LookupId_AnswerType: 0,
  Description: "",
  Author: "",
  IsMaster: false,
  IsActive: false,
  QuestionId: 0
};

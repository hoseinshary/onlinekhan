import IWriter from "./IWriter";

export default interface IQuestionAnswerMulti {
  Title: string;
  QuestionGroupId: number;
  WriterId: number;
  Writer?: IWriter;
}

export const DefaultQuestionAnswerMulti: IQuestionAnswerMulti = {
  Title: "",
  WriterId: 0,
  QuestionGroupId: 0
};

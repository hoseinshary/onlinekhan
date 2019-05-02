export default interface IQuestionAnswerMulti {
  Title: string;
  Author: string;
  QuestionGroupId: number;
}

export const DefaultQuestionAnswerMulti: IQuestionAnswerMulti = {
  Title: "",
  Author: "",
  QuestionGroupId: 0
};

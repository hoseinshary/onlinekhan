export default interface IQuestionOption {
  Id: number;
  Context: string;
  IsAnswer: boolean;
}

export const DefaultQuestionOption: IQuestionOption = {
  Id: 0,
  Context: "",
  IsAnswer: false
};

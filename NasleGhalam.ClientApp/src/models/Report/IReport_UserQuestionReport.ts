export default interface IReport_UserQuestionReport {
  Name: string;
  Family: string;
  NumberOfQuestionJudged: number;
  NumberOfQuestionAnswerJudged: number;
  NumberOfWriteQuestion: number;
  NumberOfSupervisorQuestion: number;
  Department: string;
  

}

export const DefaultReport_UserQuestionReport: IReport_UserQuestionReport = {

  Name: "",
  Family: "",
  NumberOfQuestionJudged: 0,
  NumberOfQuestionAnswerJudged: 0,
  NumberOfWriteQuestion: 0,
  NumberOfSupervisorQuestion: 0,
  Department :""
};

export default interface IPanel {
  CountAllJudged: number;
  CountAllJudges: number;
  CountAllQuestions: number;
  CountAllActiveQuestions: number;
  
}

export const DefaultPanel: IPanel = {
  CountAllJudged: 0,
  CountAllActiveQuestions : 0,
  CountAllJudges : 0,
  CountAllQuestions : 0
  
};

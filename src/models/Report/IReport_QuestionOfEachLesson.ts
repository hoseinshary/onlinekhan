export default interface IReport_QuestionOfEachLesson {
  Name: string;
  AllQuestionNum: number;
  AllQuestionTopiced: number;
  AllQuestionJudged: number;
  AllQuestionActived: number;
  AllQuestionHybrid: number;
  AllQuestionJudgedFull: number;

}

export const DefaultReport_QuestionOfEachLesson: IReport_QuestionOfEachLesson = {

  Name: "",
  AllQuestionNum: 0,
  AllQuestionTopiced: 0,
  AllQuestionJudged: 0,
  AllQuestionActived: 0,
  AllQuestionHybrid: 0,
  AllQuestionJudgedFull: 0
};

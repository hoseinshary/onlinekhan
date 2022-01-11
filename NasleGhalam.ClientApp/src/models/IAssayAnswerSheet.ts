import IAssayCrete from "./IAssay";
import utilities from "src/utilities";
import IUser, { DefaultUser } from "./IUser";
import { Tashih } from "src/utilities/enumeration";


export default interface IAssayAnswerSheet {
  Id: number;
  AssayId: number;
  // Assay?: IAssayCrete;
  UserId: number;
  User?: IUser;
  AssayVarient : number;
  AssayTime: string;
  DateTime: string;
  AnswerTimes:Array<number>;
  Answers:Array<string>;
  MaybeList:Array<boolean>;
  AfterList:Array<boolean>;
  CantList:Array<boolean>;
  QuestionIds:Array<number>;



  AnswerSheetCorectExams : Array<AssayAnswerSheetCorectExam>;

}

export const DefaultAssayAnswerSheet: IAssayAnswerSheet = {
  Id: 0,
  AssayId:0  ,
  UserId:0,
  AssayVarient:0,
  AssayTime:"",
  AnswerTimes:[],
  Answers:[],
  MaybeList:[],
  AfterList:[],
  CantList:[],
  DateTime: "",
  // Assay:utilities.cloneObject(),
  User:utilities.cloneObject(DefaultUser),
  AnswerSheetCorectExams:[],
  QuestionIds:[]

};


export class AssayAnswerSheetCorectExam {
  
  Tashih :Tashih;
  NumberOfQuestion : number;
  Path : string;
  AnswerPath : string;
  CorrectAnswer : string;
  /**
   * constructor
   */
  constructor() {
    this.Tashih = 0,
    this.NumberOfQuestion = 0,
    this.Path = "",
    this.AnswerPath = "",
    this.CorrectAnswer = ""
  }
}


export class AssayAnswerSheetReport {
  
  Id:number;
  Title:string;
  AssayId:number;
  UserId:number;
  AssayVarient : number;
  AssayTime: string;
  DateTime: string;
  ExamScore:number;
  CorrectScore:number;
  WrongScore:number;
  NonScore:number;
  QuestionIds:Array<number>;

  /**
   * constructor
   */
  constructor() {
  this.Id=0;
  this.Title ="";
  this.AssayId = 0;
  this.UserId = 0;
  this.AssayVarient = 0;
  this.AssayTime = "";
  this.DateTime = "";
  this.ExamScore = 0;
  this.CorrectScore =0;
  this.WrongScore =0;
  this.NonScore =0;
  this.QuestionIds=[];
  }
}




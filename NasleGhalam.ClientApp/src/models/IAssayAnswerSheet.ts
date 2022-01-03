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
  AnswerSheetCorectExams:[]

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

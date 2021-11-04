import IAssayCrete from "./IAssay";
import utilities from "src/utilities";
import IUser, { DefaultUser } from "./IUser";
import AssayCreate from "./IAssay";

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
  Answers:Array<number>;

}

export const DefaultAssayAnswerSheet: IAssayAnswerSheet = {
  Id: 0,
  AssayId:0  ,
  UserId:0,
  AssayVarient:0,
  AssayTime:"",
  AnswerTimes:[],
  Answers:[],
  DateTime: "",
  // Assay:utilities.cloneObject(),
  User:utilities.cloneObject(DefaultUser)
};

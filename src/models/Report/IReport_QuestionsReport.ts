import { NumberFormatResult } from "vue-i18n";

export default interface IReport_QuestionsReport {
  Id : number ;
  IsDelete : boolean;
  UserId : number;
IsActive : boolean;
IsUpdate : boolean;

LookupId_AuthorType: number;
AuthorTypeName:string;
WriterId : number;
WriterName : string ;
SupervisorUserId : number;
SupervisorName:string;

}

export const DefaultReport_QuestionsReport: IReport_QuestionsReport = {

  Id:0,
  AuthorTypeName:"",
  IsActive:false,
  IsDelete:false,
  IsUpdate:false,
  LookupId_AuthorType : 0,
  SupervisorName:"",
  SupervisorUserId:0,
  UserId:0,
  WriterId:0,
  WriterName:""

};

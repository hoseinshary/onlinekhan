import IProgram , { DefaultProgram }from "./IProgram";
import utilities from "src/utilities";
import {
DayOfWeek
} from "src/utilities/enumeration";

export default interface IProgramItem {
  Id: number;
  LookupId_PrgoramItemName: number;
  ProgramId: number;
  Program ?: IProgram;
  Hour: number;
  Description: string;
  DayOfWeak: DayOfWeek;

  
}

export const DefaultProgramItem: IProgramItem = {
  Id: 0,
  LookupId_PrgoramItemName: 0,
  ProgramId: 0,
  Program: utilities.cloneObject(DefaultProgram),
  Hour: 0,
  Description: "",
  DayOfWeak: DayOfWeek["شنبه"],
  
};

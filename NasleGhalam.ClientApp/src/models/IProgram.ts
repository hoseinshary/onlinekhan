
import IStudent , {DefaultStudent} from "./IStudent";
import IProgramItem from "./IProgramItem";
import utilities from "src/utilities";

export default interface IProgram {
  Id: number;
  Name: string;
  IsMain: boolean;
  StudentId: number;
  Student ?: IStudent;
  CreatedTime : string;
  Description: string;
  ProgramItems : Array<IProgramItem>;

  
}

export const DefaultProgram: IProgram = {
  Id: 0,
  Name: "",
  IsMain: false,
  StudentId: 0,
  Student: utilities.cloneObject(DefaultStudent),
  CreatedTime: "",
  Description: "",
  ProgramItems: []
};

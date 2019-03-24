import IProvince, { DefaultProvince } from "./IProvince";
import utilities from "src/utilities";

export default interface ICity {
  Id: number;
  Name: string;
  Province?: IProvince;
}

export const DefaultCity: ICity = {
  Id: 0,
  Name: "",
  Province: utilities.cloneObject(DefaultProvince)
};

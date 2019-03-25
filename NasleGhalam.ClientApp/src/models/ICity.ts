import IProvince, { DefaultProvince } from "./IProvince";
import utilities from "src/utilities";

export default interface ICity {
  Id: number;
  Name: string;
  ProvinceId: number;
  Province?: IProvince;
}

export const DefaultCity: ICity = {
  Id: 0,
  Name: "",
  ProvinceId: 0,
  Province: utilities.cloneObject(DefaultProvince)
};

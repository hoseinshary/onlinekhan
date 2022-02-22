export default interface IEducationYear {
  Id: number;
  Name: string;
  IsActiveYear: boolean;
}

export const DefaultEducationYear: IEducationYear = {
  Id: 0,
  Name: "",
  IsActiveYear: false
};

export default interface ITeacherGroup {
  Id: number;
  Name: string;
  TeacherId : number,
  StudentsId:Array<number>
}

export const DefaultTeacherGroup: ITeacherGroup = {
  Id: 0,
  Name: "",
  TeacherId : 0,
  StudentsId:[2345]
};

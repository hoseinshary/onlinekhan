export default interface ITag {
  Id: number;
  Name: string;
  IsSource: boolean;
  IsSourceStr: string;
}

export const DefaultTag: ITag = {
  Id: 0,
  Name: "",
  IsSource: false,
  IsSourceStr: ""
};

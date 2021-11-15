export default interface IWriter {
  Id: number;
  Name: string;
  UserId?: number | null;
  WriterPicturePath : string;
  ProfilePic: string;
}

export const DefaultWriter: IWriter = {
  Id: 0,
  Name: "",
  UserId: null,
  ProfilePic : "",
  WriterPicturePath : ""
};

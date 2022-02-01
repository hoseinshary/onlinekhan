export default interface IAxillaryBook {
  Id: number;
  Name: string;
  PublishYear: number;
  Author: string;
  Isbn: string;
  Font: string;
  LookupId_PrintType: number;
  Price: number;
  OriginalPrice: number;
  LookupId_BookType: number;
  LookupId_PaperType: number;
  Description: string;
  PublisherId: number;
  ImgPath: string;
}

export const DefaultAxillaryBook: IAxillaryBook = {
  Id: 0,
  Name: "",
  PublishYear: 1390,
  Author: "",
  Isbn: "",
  Font: "",
  LookupId_PrintType: 0,
  Price: 0,
  OriginalPrice: 0,
  LookupId_BookType: 0,
  LookupId_PaperType: 0,
  Description: "",
  PublisherId: 0,
  ImgPath: ""
};

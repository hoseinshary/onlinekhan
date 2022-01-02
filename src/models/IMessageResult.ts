import { MessageType } from "src/utilities/enumeration";

export default interface IMessageResult {
  Message: string;
  MessageType: MessageType;
  // Id?: number;
  Obj?: any;
}

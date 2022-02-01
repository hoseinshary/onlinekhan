export default interface ISubMenu {
  ControllerId: number;
  ModuleId: number;
  FaName: string;
  EnName: string;
  Icon?: string;
  UserAccess: Array<string>;
}

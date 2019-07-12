import { VuexModule, Module } from "vuex-class-component";
import AssayCreate from "src/models/IAssay";

@Module({ namespacedPath: "assayStore/" })
export class AssayStore extends VuexModule {
  assayCreate: AssayCreate;

  /**
   * initialize data
   */
  constructor() {
    super();
    this.assayCreate = new AssayCreate();
  }

  //#region ### getters ###
  get modelName() {
    return "آزمون";
  }
  //#endregion

  //#region ### mutations ###

  //#endregion

  //#region ### actions ###

  //#endregion
}

export const assayStore = AssayStore.ExtractVuexModule(AssayStore);

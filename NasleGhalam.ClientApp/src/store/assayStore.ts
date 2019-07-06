import { VuexModule, Module } from "vuex-class-component";

@Module({ namespacedPath: "assayStore/" })
export class AssayStore extends VuexModule {
  /**
   * initialize data
   */
  constructor() {
    super();
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

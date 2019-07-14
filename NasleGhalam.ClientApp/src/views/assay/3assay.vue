<template>
  <section class="row gutter-md">
    <base-input :model="$v.assayCreate.Title" class="col-md-6" />
    <base-input :model="$v.assayCreate.Time" class="col-md-6" />
    <base-select
      :model="$v.assayCreate.LookupId_Importance"
      :options="lookupStore.assayImportanceDdl"
      filter
      class="col-md-4"
    />
    <base-select
      :model="$v.assayCreate.LookupId_QuestionType"
      :options="lookupStore.questionTypeDdl"
      filter
      class="col-md-4"
    />
    <base-select
      :model="$v.assayCreate.LookupId_Type"
      :options="lookupStore.assayTypeDdl"
      filter
      class="col-md-4"
    />
    <base-field class="col-md-4" :model="$v.assayCreate.IsOnline">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model" :val="false" label="خیر" />
        <q-radio v-model="data.obj.$model" :val="true" label="بلی" />
      </template>
    </base-field>
    <base-field class="col-md-4" :model="$v.assayCreate.IsPublic">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model" :val="false" label="خیر" />
        <q-radio v-model="data.obj.$model" :val="true" label="بلی" />
      </template>
    </base-field>
    <base-field class="col-md-4" :model="$v.assayCreate.RandomOptions">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model" :val="false" label="خیر" />
        <q-radio v-model="data.obj.$model" :val="true" label="بلی" />
      </template>
    </base-field>
  </section>
</template>

<script lang="ts">
import { Vue, Component, Watch } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";
import { assayValidations } from "src/validations/assayValidation";

@Component({
  validations: assayValidations
})
export default class AssayTabVue extends Vue {
  $v: any;

  //#region ### data ###
  assayStore = vxm.assayStore;
  assayCreate = vxm.assayStore.assayCreate;
  lookupStore = vxm.lookupStore;
  //#endregion

  //#region ### computed ###
  //#endregion

  //#region ### watch ###
  //#endregion

  //#region ### hooks ###
  created() {
    this.lookupStore.fillQuestionType();
    this.lookupStore.fillAssayImportance();
    this.lookupStore.fillAssayType();
  }
  //#endregion
}
</script>
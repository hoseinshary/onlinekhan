<template>

<bs-modal
    title="ثبت نهایی آزمون "
    :show="assayStore.openModal._3assay"
    size="lg"
    @close="assayStore.OPEN_MODAL_3ASSAY(false)"
 
  >

   <template slot="header">
      <q-toolbar slot="header" color="primary" text-color>
        <q-toolbar-title>ثبت نهایی آزمون</q-toolbar-title>
        <q-btn dense icon="close" @click="assayStore.OPEN_MODAL_3ASSAY(false)" />
      </q-toolbar>
    </template>
<slot>
  <section class="row gutter-md">
    <base-input :model="$v.assayCreate.Title" class="col-md-6" />
    <base-input :model="$v.assayCreate.Time" class="col-md-6" />
    <base-select
      :model="$v.assayCreate.LookupId_Importance"
      :options="lookupStore.assayImportanceDdl"
      class="col-md-4"
    />
    <base-select
      :model="$v.assayCreate.LookupId_QuestionType"
      :options="lookupStore.questionTypeDdl"
      class="col-md-4"
    />
    <base-select
      :model="$v.assayCreate.LookupId_Type"
      :options="lookupStore.assayTypeDdl"
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
     <base-select
      :model="$v.assayCreate.NumberOfVarient"
      :options="numberOfVarientDdl"
      class="col-md-4"
    />
    <base-select :model="$v.assayCreate.Font" :options="fontDdl" class="col-md-4" />
    <base-input :model="$v.assayCreate.FontSize" class="col-md-4" />
    <base-field class="col-md-4" :model="$v.assayCreate.TwoPageInOne">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model" :val="false" label="خیر" />
        <q-radio v-model="data.obj.$model" :val="true" label="بلی" />
      </template>
    </base-field>

    <!-- <base-field class="col-md-6" :model="$v.assayCreate.QuestionsRelocation">
      <template slot-scope="data">
        <q-checkbox v-model="data.obj.$model" />
        <q-radio
          v-show="data.obj.$model"
          v-model="assayCreate.QuestionsRelocationMode"
          :val="true"
          label="در مبحث"
        />
        <q-radio
          v-show="data.obj.$model"
          v-model="assayCreate.QuestionsRelocationMode"
          :val="false"
          label="در درس"
        />
      </template>
    </base-field> -->

    <base-field class="col-md-6" :model="$v.assayCreate.HaveWhiteSpace">
      <template slot-scope="data">
        <q-checkbox v-model="data.obj.$model" />
        <q-radio
          v-show="data.obj.$model"
          v-model="assayCreate.WhiteSpacePosition"
          :val="true"
          label="بغل"
        />
        <q-radio
          v-show="data.obj.$model"
          v-model="assayCreate.WhiteSpacePosition"
          :val="false"
          label="زیر"
        />
      </template>
    </base-field>

    <div class="col-12">
      <base-btn-save @click="submitAssay" />
    </div>
  </section>
</slot>
</bs-modal>
</template>

<script lang="ts">
import { Vue, Component, Watch } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";
import { assayValidations } from "src/validations/assayValidation";
import { AssayVarient, Fonts } from "src/utilities/enumeration";

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
  get fontDdl() {
    return Object.keys(Fonts)
      .reduce((arr: Array<any>, key: string) => {
        if (!arr.includes(key)) {
          arr.push(Fonts[key]);
        }
        return arr;
      }, [])
      .map(x => ({
        label: x,
        value: Fonts[x]
      }));
  }

   get numberOfVarientDdl() {
    return util.enumToDdl2(AssayVarient);
  }
  //#endregion

  //#region ### methods ###
  submitAssay() {
    this.assayStore.submitCreate().then(() => {
      this.$emit("changeTab", "questionTab");
    });
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.lookupStore.fillQuestionType();
    this.lookupStore.fillAssayImportance();
    this.lookupStore.fillAssayType();
    this.assayStore.SET_ASSAY_VUE(this);
  }
  //#endregion
}
</script>
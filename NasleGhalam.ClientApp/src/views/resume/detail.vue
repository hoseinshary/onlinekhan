<template>
  <bs-modal :title="resumeStore.modelName" :show="resumeStore.openModal.detail" size="xl">
    <template slot="header">
      <q-toolbar slot="header" color="cyan-9">
        <q-toolbar-title>جزییات رزومه</q-toolbar-title>
        <q-btn dense icon="close" @click="resumeStore.OPEN_MODAL_DETAIL(false)" />
      </q-toolbar>
    </template>
    <base-input :model="$v.resume.Branch" class="col-md-3" />
    <base-input :model="$v.resume.Name" class="col-md-3" />
    <base-input :model="$v.resume.Family" class="col-md-3" />
    <base-input :model="$v.resume.FatherName" class="col-md-3" />
    <base-input :model="$v.resume.IdNumber" class="col-md-3" />
    <base-input :model="$v.resume.NationalNo" class="col-md-3" />
    <base-field class="col-md-3" :model="$v.resume.Gender">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model" :val="false" label="زن" />
        <q-radio v-model="data.obj.$model" :val="true" label="مرد" />
      </template>
    </base-field>

    <base-input :model="$v.resume.Phone" class="col-md-3" helper="به همراه کد استان" />
    <base-input :model="$v.resume.Mobile" class="col-md-3" />
    <base-input :model="$v.resume.CityBorn" class="col-md-3" />
    <base-input :model="$v.resume.PBirthday" class="col-md-3" />
    <base-field class="col-md-3" :model="$v.resume.Marriage">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model" :val="false" label="مجرد" />
        <q-radio v-model="data.obj.$model" :val="true" label="متاهل" />
      </template>
    </base-field>

    <base-input :model="$v.resume.Religion" class="col-md-3" />

    <fieldset class="col-12 q-my-sm">
      <legend>آدرس</legend>
      <section class="row gutter-sm">
        <base-select
          :model="$v.resume.ProvinceId"
          :options="provinceStore.ddl"
          class="col-md-4"
          filter
          @change="resume.CityId=0"
        />
        <base-select
          :model="$v.resume.CityId"
          :options="cityStore.ddlByProvinceId(resume.ProvinceId)"
          class="col-md-4"
          filter
        />
        <base-input :model="$v.resume.PostCode" class="col-md-4" />
        <base-input :model="$v.resume.Address" class="col-md-12" />
      </section>
    </fieldset>

    <fieldset class="col-12 q-my-sm">
      <legend>مشخصات پدر</legend>
      <section class="row gutter-sm">
        <base-input :model="$v.resume.FatherJob" class="col-md-4" />
        <base-select :model="$v.resume.FatherDegree" :options="degreeDdl" class="col-md-4" />
        <base-input :model="$v.resume.FatherPhone" class="col-md-4" />
      </section>
    </fieldset>

    <fieldset class="col-12 q-my-sm">
      <legend>مشخصات مادر</legend>
      <section class="row gutter-sm">
        <base-input :model="$v.resume.MotherJob" class="col-md-4" />
        <base-select :model="$v.resume.MotherDegree" :options="degreeDdl" class="col-md-4" />
        <base-input :model="$v.resume.MotherPhone" class="col-md-4" />
      </section>
    </fieldset>

    <fieldset class="col-12 q-my-sm" v-show="resume.Marriage">
      <legend>مشخصات همسر</legend>
      <section class="row gutter-sm">
        <base-input :model="$v.resume.PartnerJob" class="col-md-4" />
        <base-select :model="$v.resume.PartnerDegree" :options="degreeDdl" class="col-md-4" />
        <base-input :model="$v.resume.PartnerPhone" class="col-md-4" />
      </section>
    </fieldset>

    <!-- <fieldset class="col-12 q-my-sm">
                  <legend>معرف1</legend>
                  <section class="row gutter-sm">
                    <base-input :model="$v.resume.Reagent1" class="col-md-4" />
                    <base-input :model="$v.resume.RelationReagent1" class="col-md-4" />
                    <base-input :model="$v.resume.JobReagent1" class="col-md-4" />
                    <base-input :model="$v.resume.PhoneReagent1" class="col-md-3" />
                    <base-input :model="$v.resume.AddressReagent1" class="col-md-9" />
                  </section>
                </fieldset>

                <fieldset class="col-12 q-my-sm">
                  <legend>معرف2</legend>
                  <section class="row gutter-sm">
                    <base-input :model="$v.resume.Reagent2" class="col-md-4" />
                    <base-input :model="$v.resume.RelationReagent2" class="col-md-4" />
                    <base-input :model="$v.resume.JobReagent2" class="col-md-4" />
                    <base-input :model="$v.resume.PhoneReagent2" class="col-md-3" />
                    <base-input :model="$v.resume.AddressReagent2" class="col-md-9" />
                  </section>
    </fieldset>-->

    <fieldset class="col-12 q-my-sm">
      <legend>گزینش آموزش و پرورش</legend>
      <section class="row gutter-sm">
        <base-field class="col-md-3" :model="$v.resume.HaveEducationCertificate">
          <template slot-scope="data">
            <q-radio v-model="data.obj.$model" :val="false" label="خیر" />
            <q-radio v-model="data.obj.$model" :val="true" label="بلی" />
          </template>
        </base-field>
        <base-field
          v-show="!resume.HaveEducationCertificate"
          class="col-md-3"
          :model="$v.resume.HaveAnotherCertificate"
        >
          <template slot-scope="data">
            <q-radio v-model="data.obj.$model" :val="false" label="خیر" />
            <q-radio v-model="data.obj.$model" :val="true" label="بلی" />
          </template>
        </base-field>
        <base-input
          v-show="resume.HaveAnotherCertificate"
          :model="$v.resume.AnotherCertificate"
          class="col-md-3"
        />
      </section>
    </fieldset>

    <fieldset class="col-12 q-my-sm">
      <legend>سابقه تالیف</legend>
      <section class="row gutter-sm">
        <base-field class="col-md-3" :model="$v.resume.HavePublication">
          <template slot-scope="data">
            <q-radio v-model="data.obj.$model" :val="false" label="خیر" />
            <q-radio v-model="data.obj.$model" :val="true" label="بلی" />
          </template>
        </base-field>
        <base-input
          v-show="resume.HavePublication"
          :model="$v.resume.NumberOfPublication"
          class="col-md-3"
        />
      </section>
    </fieldset>

    <publication v-show="resume.HavePublication"></publication>

    <fieldset class="col-12 q-my-sm">
      <legend>سابقه تدریس</legend>
      <section class="row gutter-sm">
        <base-field class="col-md-3" :model="$v.resume.HaveTeachingResume">
          <template slot-scope="data">
            <q-radio v-model="data.obj.$model" :val="false" label="خیر" />
            <q-radio v-model="data.obj.$model" :val="true" label="بلی" />
          </template>
        </base-field>
        <base-input
          v-show="resume.HaveTeachingResume"
          :model="$v.resume.NumberOfTeachingYear"
          class="col-md-3"
        />
      </section>
    </fieldset>

    <teaching-resume v-show="resume.HaveTeachingResume"></teaching-resume>

    <fieldset class="col-12 q-my-sm">
      <legend>تقاضای تدریس یا تالیف 1</legend>
      <section class="row gutter-sm">
        <base-field class="col-md-3" :model="$v.resume.TeachingOrPublishingRequest1">
          <template slot-scope="data">
            <q-radio v-model="data.obj.$model" :val="false" label="تالیف" />
            <q-radio v-model="data.obj.$model" :val="true" label="تدریس" />
          </template>
        </base-field>
        <base-select :model="$v.resume.MaghtaRequest1" :options="maghtaDdl" class="col-md-3" />
        <base-select :model="$v.resume.KindRequest1" :options="kindRequestDdl" class="col-md-3" />
        <base-input :model="$v.resume.LessonNameRequest1" class="col-md-3" />
      </section>
    </fieldset>

    <fieldset class="col-12 q-my-sm">
      <legend>تقاضای تدریس یا تالیف 2</legend>
      <section class="row gutter-sm">
        <base-field class="col-md-3" :model="$v.resume.TeachingOrPublishingRequest2">
          <template slot-scope="data">
            <q-radio v-model="data.obj.$model" :val="false" label="تالیف" />
            <q-radio v-model="data.obj.$model" :val="true" label="تدریس" />
          </template>
        </base-field>
        <base-select :model="$v.resume.MaghtaRequest2" :options="maghtaDdl" class="col-md-3" />
        <base-select :model="$v.resume.KindRequest2" :options="kindRequestDdl" class="col-md-3" />
        <base-input :model="$v.resume.LessonNameRequest2" class="col-md-3" />
      </section>
    </fieldset>

    <fieldset class="col-12 q-my-sm">
      <legend>درخواست برای مشاوره</legend>
      <section class="row gutter-sm">
        <base-field class="col-md-3" :model="$v.resume.RequestForAdvice">
          <template slot-scope="data">
            <q-radio v-model="data.obj.$model" :val="false" label="خیر" />
            <q-radio v-model="data.obj.$model" :val="true" label="بلی" />
          </template>
        </base-field>
        <base-select
          v-show="resume.RequestForAdvice"
          :model="$v.resume.MaghtaAdvice"
          :options="maghtaDdl"
          class="col-md-3"
        />
      </section>
    </fieldset>

    <base-input :model="$v.resume.Description" class="col-12" />
    <education-certificate></education-certificate>

    <template slot="footer">
      <base-btn-back @click="resumeStore.OPEN_MODAL_DETAIL(false)"></base-btn-back>
    </template>
  </bs-modal>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { resumeValidations } from "src/validations/ResumeValidation";
import util from "src/utilities";
import {
  Degree,
  Maghta,
  KindRequest,
  DegreeCertificate,
  TypeEducationCenter
} from "../../utilities/enumeration";

@Component({
  validations: resumeValidations,
  components: {
    Publication: () => import("./publication.vue"),
    EducationCertificate: () => import("./educationCertificate.vue"),
    TeachingResume: () => import("./teachingResume.vue")
  }
})
export default class ResumeEditVue extends Vue {
  $v: any;

  //#region ### data ###
  resumeStore = vxm.resumeStore;
  resume = vxm.resumeStore.resume;
  cityStore = vxm.cityStore;
  provinceStore = vxm.provinceStore;
  //#endregion

  //#region ### computed ###
  get degreeDdl() {
    return util.enumToDdl(Degree);
  }

  get maghtaDdl() {
    return util.enumToDdl(Maghta);
  }

  get kindRequestDdl() {
    return util.enumToDdl(KindRequest);
  }

  get degreeCertificateDdl() {
    return util.enumToDdl(DegreeCertificate);
  }

  get typeEducationCenterDdl() {
    return util.enumToDdl(TypeEducationCenter);
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.cityStore.fillList();
    this.provinceStore.fillList();
  }
  //#endregion
}
</script>


<template>
  <fieldset class="col-12 q-my-sm">
    <legend>مدارک تحصیلی</legend>
    <section class="row gutter-sm">
      <base-select
        :model="$v.educationCertificate.DegreeCertificate"
        :options="degreeCertificateDdl"
        class="col-md-3"
      />
      <base-input :model="$v.educationCertificate.Subject" class="col-md-3" />
      <base-input :model="$v.educationCertificate.EducationCenterName" class="col-md-3" />
      <base-input :model="$v.educationCertificate.CityName" class="col-md-3" />
      <base-select
        :model="$v.educationCertificate.TypeEducationCenter"
        :options="typeEducationCenterDdl"
        class="col-md-3"
      />
      <base-input :model="$v.educationCertificate.Year" class="col-md-3" />
      <base-input :model="$v.educationCertificate.Score" class="col-md-3" />

      <div class="col-3">
        <base-btn-create label="ایجاد" @click="resumeStore.addEducationCertificate" />
      </div>

      <div class="col-12">
        <table class="my-table">
          <thead>
            <tr>
              <th>مدرک</th>
              <th>رشته</th>
              <th>مرکز تحصیلی</th>
              <th>شهر</th>
              <th>نوع</th>
              <th>سال</th>
              <th>معدل</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(educationCertificate, index) in educationCertificateList" :key="index">
              <td>{{degreeCertificateName(educationCertificate.DegreeCertificate)}}</td>
              <td>{{educationCertificate.Subject}}</td>
              <td>{{educationCertificate.EducationCenterName}}</td>
              <td>{{educationCertificate.CityName}}</td>
              <td>{{typeEducationCenterName(educationCertificate.TypeEducationCenter)}}</td>
              <td>{{educationCertificate.Year}}</td>
              <td>{{educationCertificate.Score}}</td>
            </tr>
          </tbody>
        </table>
      </div>
    </section>
  </fieldset>
</template>

<script lang="ts">
import { Vue, Component, Watch } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";
import { educationCertificateValidations } from "src/validations/ResumeValidation";
import {
  KindRequest,
  DegreeCertificate,
  TypeEducationCenter
} from "../../utilities/enumeration";

@Component({
  validations: educationCertificateValidations
})
export default class EducationCertificateVue extends Vue {
  $v: any;

  //#region ### data ###
  resumeStore = vxm.resumeStore;
  educationCertificate = vxm.resumeStore.educationCertificate;
  //#endregion

  //#region ### computed ###
  get degreeCertificateDdl() {
    return util.enumToDdl(DegreeCertificate);
  }

  get typeEducationCenterDdl() {
    return util.enumToDdl(TypeEducationCenter);
  }

  get educationCertificateList() {
    return this.resumeStore.resume.EducationCertificates;
  }

  get degreeCertificateName() {
    return (index: number) => {
      return DegreeCertificate[index];
    };
  }

  get typeEducationCenterName() {
    return (index: number) => {
      return TypeEducationCenter[index];
    };
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.resumeStore.SET_EDUCATION_CERTIFICATE_VUE(this);
  }
  //#endregion
}
</script>
<template>
  <section class="col-md-8">
  <q-list highlight  >
  <q-list-header>لیست رشته های انتخاب شده  
    
    
      </q-list-header>

  <q-item v-for="major in selectedMajors" :key="major.Id" >
    <q-item-side>
      {{selectedMajors.indexOf(major)+1}}
    </q-item-side>
        <q-item-main :label="major.Code.toString()" />

            <q-item-main :label="major.Apply" />

    <q-item-main :label="major.Course" />
    <q-item-main :label="major.MajorTitle" />
    <q-item-main :label="major.University" />
    <q-item-main :label="major.AdmissionFirst.toString()" />
    <q-item-main :label="major.AdmissionSecond.toString()" />
    <q-item-main :label="major.Man" />
    <q-item-main :label="major.Woman" />


    <q-item-main :label="major.Description" />


  </q-item>
 
  
 
</q-list>
  </section>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";

@Component({
  components: {

  }
})
export default class PrintMajorListVue extends Vue {
  //#region ### data ###
  studentMajorListStore = vxm.studentMajorListStore;
   studentMajorList = vxm.studentMajorListStore.studentMajorList;

  //#endregion

  //#region ### computed ###
  get selectedMajors()
  {
    return this.studentMajorList.Majors;
  }  
  //#endregion

  //#region ### methods ###
  
  //#endregion

  //#region ### hooks ###
  created() {
    
  }
  //#endregion
}
</script>
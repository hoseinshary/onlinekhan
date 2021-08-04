<template>
  <base-modal-create
    :title="studentMajorListStore.modelName"
    :show="studentMajorListStore.openModal.create"
    size="xl"

    @confirm="studentMajorListStore.submitCreate"
    @reset="studentMajorListStore.resetCreate"
    @close="studentMajorListStore.OPEN_MODAL_CREATE(false)"
  >

  <div class="col-md-12" >

    <div class="col-md-5">
       <!-- <q-select
        v-model="fieldFilter"
        :options="fieldDdl"
        class="col-md-3"
      /> -->
      
      <q-input float-label="نام رشته/دانشگاه" v-model="nameFilter" />
       </div>
      <br/>
      <q-btn color="primary" icon="search" label="جستجو" @click="fillGrid(nameFilter)" />
 
  </div>
  <div class="col-md-12 row">
<div class="col-md-7 q-ml-sm">
   
    <base-table :grid-data="studentMajorListStore.gridDataMajor" :columns="MajorListGridColumn" hasIndex>
      <template slot="Id" slot-scope="data">
        <q-btn round color="secondary" icon="add" @click="addToTable(data.row.Id)"/>

      </template>
    </base-table>
  </div>
  <div class="com-md-1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </div>
  <div class="col-md-4">
    <div class=""><br/> </div>
    <div class=""> <br/></div>
    <base-input :model="$v.studentMajorList.Title" float-label="نام لیست" class="col-md-6"/>
        <div class=""> <br/></div>
<label class="text-red">تعداد رشته های انتخاب شده : {{selectedMajors.length}}</label>
        <div class=""> <br/></div>

<q-list highlight >
  <q-list-header>لیست رشته های انتخاب شده</q-list-header>

  <q-item v-for="major in selectedMajors" :key="major.Id" >
    <q-item-side>
      {{selectedMajors.indexOf(major)+1}}
    </q-item-side>
    <q-item-main :label="major.MajorTitle" />
    <q-item-main :label="major.University" />
    <q-item-main :label="major.Code.toString()" />
    <q-item-side right>
      
    <q-btn round color="negative" icon="remove" @click="deleteFromTable(major.Id)"/>
    </q-item-side>
  </q-item>
 
  
 
</q-list>



    <!-- <q-table
    
    title="لیست رشته های انتخاب شده"
    v-model="$v.studentMajorList.Majors"
    :columns="MajorListGridColumn2"
    row-key="Id"
  /> -->
  </div>
  </div>
  
   
    
   
  </base-modal-create>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";

import { studentMajorListValidations } from "src/validations/StudentMajorListValidation";
import { Field } from "src/utilities/enumeration";

@Component({
  validations: studentMajorListValidations
})
export default class StudentMajorListCreateVue extends Vue {
  $v: any;

  //#region ### data ###
  studentMajorListStore = vxm.studentMajorListStore;
  studentMajorList = vxm.studentMajorListStore.studentMajorList;

  fieldFilter  = Field["تجربی"];
  nameFilter  = "";


  MajorListGridColumn = [
    {
      title: "دوره",
      data: "Course"
    },
    {
      title: "کد",
      data: "Code",
            searchable: true,
            sortable:true

    },
    {
      title: "نام رشته",
      data: "MajorTitle",
            searchable: true,
            sortable:true
    },
    {
      title: "نیمسال اول",
      data: "AdmissionFirst"
    },
     {
      title: "نیمسال دوم",
      data: "AdmissionSecond"
    },
    {
      title: "مرد",
      data: "Man"
    },
    {
      title: "زن",
      data: "Woman"
    },
     {
      title: "دانشگاه",
      data: "University",
            searchable: true,
            sortable:true
    },
    {
      title: "عملیات",
      data: "Id",
      searchable: false,
      sortable: false
      
    }
  ];
   MajorListGridColumn2 = [
    
    {
      label: "کد",
      name: "Code",
      searchable: false,
      sortable: false
    },
    {
      label: "نام رشته",
      name: "MajorTitle",
      searchable: false,
      sortable: false
    },
    
     {
      label: "دانشگاه",
      name: "University",
      searchable: false,
      sortable: false
    },
    {
      label: "حذف",
      name: "Id",
      searchable: false,
      sortable: false
      
    }
  ];
  //#endregion

   get fieldDdl() {
    return util.enumToDdl(Field);
  }

get selectedMajors()
{
  return this.studentMajorList.Majors;
}
  fillGrid(text:any)
  {
    
    this.studentMajorListStore.getMajorsBySearch(text);    
        

  }
  addToTable(Id:number)
  {
    
    var tempMajor = this.studentMajorListStore.gridDataMajor.find(x=>x.Id ===Id);
    
    if(tempMajor && !this.studentMajorList.Majors.find(x => x.Id === Id))
    {
      
      this.studentMajorList.Majors.push(tempMajor);    
    }
    
  }

   deleteFromTable(Id:number)
  {
    
    var tempMajor = this.studentMajorList.Majors.findIndex(x=>x.Id ===Id);
    
    if(tempMajor >=0)
    {
      
      this.studentMajorList.Majors.splice(tempMajor ,1);    
    }
    
    
  }
  //#region ### hooks ###
  created() {
    this.studentMajorListStore.SET_CREATE_VUE(this);
  }
  //#endregion
}
</script>


<template>
  <base-modal-edit
    :title="studentMajorListStore.modelName"
    :show="studentMajorListStore.openModal.edit"
    @confirm="studentMajorListStore.submitEdit"
    @reset="studentMajorListStore.resetEdit"
    size="xl"
    @close="studentMajorListStore.OPEN_MODAL_EDIT(false)"
  >
  <div class="col-md-12" >

    <div class="col-md-5">
       <!-- <q-select
        v-model="fieldFilter"
        :options="fieldDdl"
        class="col-md-3"
      /> -->
          <q-select
        v-model="searchfilter.fieldFilter"
        :options="fieldDdl"
        class="col-md-3"
      />
        
    <q-select
      v-model="searchfilter.history"
      float-label="با آزمون / سوابق تحصیلی"
      class="col-md-3"
     :options="historyDdl"
    />
      <q-input float-label="نام رشته/دانشگاه" v-model="searchfilter.nameFilter" />
       </div>
      <br/>
      <q-btn color="primary" icon="search" label="جستجو" @click="fillGrid(searchfilter)" />
 
  </div>
  <div class="col-md-12 row" v-if="!getLargList">
<div class="col-md-7 q-ml-sm">
   
    <base-table :grid-data="studentMajorListStore.gridDataMajor" :columns="MajorListGridColumn" hasIndex>
      <template slot="Id" slot-scope="data">
        <q-btn round color="secondary" icon="add" @click="addToTable(data.row.Id)"/>

      </template>
    </base-table>
  </div>
  <div class="com-md-1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </div>
  <div class="col-md-4" >
    <div class=""><br/> </div>
    <div class=""> <br/></div>
    <base-input :model="$v.studentMajorList.Title" class="col-md-6"/>
        <div class=""> <br/></div>
<label class="text-red">تعداد رشته های انتخاب شده : {{selectedMajors.length}}</label>

<q-btn class="q-ma-sm btn float-right" @click="changeLargList">
          بزرگ نمایی              

  </q-btn>
        <div class=""> <br/></div>
<q-list highlight  >
  <q-list-header>لیست رشته های انتخاب شده  
    
    
      </q-list-header>

  <q-item v-for="major in selectedMajors" :key="major.Id" >
    <q-item-side>
      {{selectedMajors.indexOf(major)+1}}
    </q-item-side>
    <q-item-main :label="major.MajorTitle" />
    <q-item-main :label="major.University" />
    <q-item-main :label="major.Code.toString()" />
        <q-item-side right>
        
              <q-btn size="sm" round dense color="secondary" icon="expand_less" @click="upList(major)" class="q-mr-xs" />
              <q-btn size="sm" round dense color="tertiary" icon="expand_more" @click="downList(major)" class="q-mr-sm" />
          </q-item-side>
    <q-item-side right>
      
    <q-btn round color="negative" icon="remove" @click="deleteFromTable(major.Id)"/>
    </q-item-side>
  </q-item>
 
  
 
</q-list>


  </div>

 
  </div>
<div class="col-md-12 row" v-else>
<div class="col-md-12 q-ml-sm">
   
    <base-table :grid-data="studentMajorListStore.gridDataMajor" :columns="MajorListGridColumn" hasIndex>
      <template slot="Id" slot-scope="data">
        <q-btn round color="secondary" icon="add" @click="addToTable(data.row.Id)"/>

      </template>
    </base-table>
  </div>
  <div class="col-md-12" >
    <div class=""><br/> </div>
    <div class=""> <br/></div>
    <base-input :model="$v.studentMajorList.Title" class="col-md-6"/>
        <div class=""> <br/></div>
<label class="text-red">تعداد رشته های انتخاب شده : {{selectedMajors.length}}</label>
<q-btn class="q-ma-sm btn float-right" @click="changeLargList">
          بزرگ نمایی              

  </q-btn>
 
        <div class=""> <br/></div>
<q-list highlight  >
  <q-list-header>لیست رشته های انتخاب شده  
    
    
      </q-list-header>

  <q-item v-for="major in selectedMajors" :key="major.Id" >
    <q-item-side>
      {{selectedMajors.indexOf(major)+1}}
    </q-item-side>
            <q-item-main :label="major.Apply" />

    <q-item-main :label="major.Course" />
    <q-item-main :label="major.Code.toString()" />
    <q-item-main :label="major.MajorTitle" />
    <q-item-main :label="major.University" />
    <q-item-main :label="major.AdmissionFirst.toString()" />
    <q-item-main :label="major.AdmissionSecond.toString()" />
    <q-item-main :label="major.Man" />
    <q-item-main :label="major.Woman" />


    <q-item-main :label="major.Description" />


        <q-item-side right>
        
              <q-btn size="sm" round dense color="secondary" icon="expand_less" @click="upList(major)" class="q-mr-xs" />
              <q-btn size="sm" round dense color="tertiary" icon="expand_more" @click="downList(major)" class="q-mr-sm" />
          </q-item-side>
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
   
  </base-modal-edit>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { studentMajorListValidations } from "src/validations/StudentMajorListValidation";
import { Field ,HistoryAssay } from "src/utilities/enumeration";
import util from "src/utilities";
import { Major } from "src/models/IStudentMajorList";
import IMessageResult from "src/models/IMessageResult";




@Component({
  validations: studentMajorListValidations
})
export default class StudentMajorListEditVue extends Vue {
  $v: any;

  //#region ### data ###
  studentMajorListStore = vxm.studentMajorListStore;
  studentMajorList = vxm.studentMajorListStore.studentMajorList;

   searchfilter ={ fieldFilter : Field["تجربی"], nameFilter : "" , history:HistoryAssay["با آزمون"]};
  largeList :boolean = false;


  MajorListGridColumn = [
     {
      title: "نحوه پذیرش",
      data: "Apply"
    },
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
      title: "توضیحات",
      data: "Description"
            
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
get selectedMajors()
{
  return this.studentMajorList.Majors;
}

get getLargList()
{
  return this.largeList;
}

get fieldDdl() {
    return util.enumToDdl(Field);
  }

   get historyDdl() {
    return util.enumToDdl(HistoryAssay);
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

        var data : IMessageResult = {
        MessageType : 1 ,
        Message : "به لیست اضافه شد "

      }
      this.studentMajorListStore.notify( {vm:this,data:data});
    }
    else
    {

        var data : IMessageResult = {
        MessageType : 2 ,
        Message : "رشته تکراری  "

      }
      this.studentMajorListStore.notify( {vm:this,data:data});
    }
    
  }
  upList(major :Major)
  {
    var currentIndex = this.studentMajorList.Majors.findIndex(x => x.Id === major.Id);

    if(currentIndex && currentIndex > 0)
    {
        this.studentMajorList.Majors.splice(currentIndex-1, 0, this.studentMajorList.Majors.splice(currentIndex, 1)[0]);
    }
  }

  changeLargList()
  {
    if(this.largeList)
     this.largeList = false;
     else
     this.largeList= true;
  }

  downList(major:any)
  {
    var currentIndex = this.studentMajorList.Majors.findIndex(x => x.Id === major.Id);

    if((currentIndex || currentIndex ==0) && currentIndex < this.studentMajorList.Majors.length-1)
    {
        this.studentMajorList.Majors.splice(currentIndex+1, 0, this.studentMajorList.Majors.splice(currentIndex, 1)[0]);
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
    this.studentMajorListStore.SET_EDIT_VUE(this);
  }
  //#endregion
}
</script>


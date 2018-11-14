<template>
  <my-modal-edit :title="modelName" size="lg" :show="isOpenModalEdit" @confirm="submitEdit" @reset="resetEditStore" @close="toggleModalEditStore(false)" @open="modalOpen">

    <div class="col-12 row">
      <div class="col-md-4 row">
        <q-slide-transition>
          <q-tree :nodes="educationTreeData" class="col-md-12" color="blue" :ticked.sync="instanceObj.EducationTreeIds" tick-strategy="leaf" accordion node-key="Id" ref="topicTree" />
          <!-- :selected.sync="selectedNodeId" -->
        </q-slide-transition>
      </div>
      <div class="col-md-8 row">
        <my-input :model="$v.instanceObj.Name" class="col-md-4" />
        <my-field class="col-md-4" :model="$v.instanceObj.IsMain">
          <template slot-scope="data">
            <q-radio v-model="data.obj.$model" :val="false" label="خیر" />
            <q-radio v-model="data.obj.$model" :val="true" label="بلی" />
          </template>
        </my-field>
        <my-select :model="$v.instanceObj.LookupId_Nezam" :options="lookupTopicNezamDdl" class="col-md-4" clearable />
        <q-slide-transition>
          <fieldset class="col-12" v-if="educationGroupList.length > 0" style="max-height: 65px;">
            <legend> گروههای آموزشی</legend>
            <div class="row">
              <div class="col-sm-2" v-for="group in educationGroupList" :key="group.Name">
                <q-checkbox v-model="group.IsChecked" :label="group.Name" />
              </div>
            </div>
          </fieldset>
        </q-slide-transition>
        <div class="col-12" v-for="group in educationGroupList.filter(x => x.IsChecked)" :key="group.Id">
          <q-slide-transition>
            <fieldset class="col-12">
              <legend>زیر گروههای آموزشی</legend>
              <div class="row">
                <div class="inline col-sm-4" style="padding-top:5px;" v-for="subGroup in group.SubGroups" :key="subGroup.EducationSubGroupId">
                  <div style="margin-top:20px;">{{subGroup.Name}}:</div>
                  <q-input type="number" style="margin-right:10px; width:20%;" v-model="subGroup.Rate" float-label='ضریب'></q-input>
                </div>
              </div>
            </fieldset>
          </q-slide-transition>
        </div>
      </div>
    </div>
  </my-modal-edit>
</template>

<script>
import viewModel from "viewModels/lessonViewModel";
import { mapState, mapActions } from "vuex";

export default {
  data() {
    return {
      selectedNodeIds: null
    };
  },
  /**
   * methods
   */
  methods: {
    ...mapActions("lessonStore", [
      "toggleModalEditStore",
      "editVueStore",
      "submitEditStore",
      "resetEditStore",
      "getAllEduGroupAndEduSubGroupStore",
      "setEducationGroupListLesson"
    ]),
    ...mapActions("lookupStore", ["fillTopicNezamStore"]),
    ...mapActions("gradeStore", {
      fillGradeDdlStore: "fillDdlStore"
    }),
    ...mapActions("gradeLevelStore", {
      fillGradeLevel: "fillDdlStore"
    }),
    ...mapActions("educationTreeStore", [
      "getEducationGroupsUnderSelectedList"
    ]),
    modalOpen: function() {
      // this.getAllEduGroupAndEduSubGroupStore();
      this.fillTopicNezamStore();
      // this.fillGradeDdlStore();
      // this.fillGradeLevel();
    },
    submitEdit: function() {
      this.setEducationGroupListLesson(this.educationGroupList);
      this.submitEditStore();
    }
  },
  /**
   * computed
   */
  computed: {
    ...mapState("educationTreeStore", {
      educationGroupList: "educationGroupList",
      educationTreeData: "educationTreeData"
    }),
    ...mapState("lessonStore", {
      modelName: "modelName",
      instanceObj: "instanceObj",
      isOpenModalEdit: "isOpenModalEdit",
      EducationGroups: "EducationGroups",
      educationGroupListLesson: "educationGroupList"
      // selectedNodeIds: "selectedNodeIds"
    }),
    ...mapState("lookupStore", ["lookupTopicNezamDdl"]),
    ...mapState("gradeStore", { gradeDdl: "gradeDdl" }),
    ...mapState("gradeLevelStore", { gradeLevelDdl: "gradeLevelDdl" }),
    gradeLavelFilteredDdl: function() {
      return this.instanceObj.GradeId == 0
        ? []
        : this.gradeLevelDdl.filter(x => x.gradeId == this.instanceObj.GradeId);
    }
  },
  /**
   * validations
   */
  validations: viewModel,
  /**
   * created
   */
  created() {
    this.editVueStore(this);
  },
  watch: {
    "instanceObj.EducationTreeIds"(newVal) {
      var eduGroupsId = [];
      this.instanceObj.Ratios.forEach(element => {
        var eduGroupId = element.EducationSubGroup.EducationTreeId;
        if (!eduGroupsId.includes(eduGroupId)) eduGroupsId.push(eduGroupId);
      });
      if (this.isOpenModalEdit)
        this.getEducationGroupsUnderSelectedList({
          lstSelected: newVal,
          isEdit: true,
          eduGroupsId: eduGroupsId,
          Ratios:this.instanceObj.Ratios
        });
    }
  }
};
</script>

<style scoped>
div >>> .inline {
  display: inline-flex;
}
</style>

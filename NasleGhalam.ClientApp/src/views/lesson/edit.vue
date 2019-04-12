<template>
  <base-modal-edit
    :title="lessonStore.modelName"
    size="lg"
    :show="lessonStore.openModal.edit"
    @confirm="lessonStore.submitEdit"
    @reset="lessonStore.resetEdit"
    @close="lessonStore.OPEN_MODAL_EDIT(false)"
    @open="open"
  >
    <div class="col-md-4">
      <q-tree
        :nodes="educationTreeStore.treeData"
        color="blue"
        :expanded.sync="expandedTreeIds"
        :ticked.sync="lesson.EducationTreeIds"
        tick-strategy="leaf"
        accordion
        node-key="Id"
        ref="educationTree"
      />
    </div>
    <div class="col-md-8">
      <section class="row gutter-sm">
        <base-input :model="$v.lesson.Name" class="col-md-4"/>
        <base-select
          :model="$v.lesson.LookupId_Nezam"
          :options="lookupStore.topicNezamDdl"
          class="col-md-4"
        />
        <base-field class="col-md-4" :model="$v.lesson.IsMain">
          <template slot-scope="data">
            <q-radio v-model="data.obj.$model" :val="false" label="خیر"/>
            <q-radio v-model="data.obj.$model" :val="true" label="بلی"/>
          </template>
        </base-field>

        <q-slide-transition>
          <fieldset class="col-12" v-show="educationTree_educationGroup.length > 0">
            <legend>گروههای آموزشی</legend>
            <div class="row">
              <div class="col-4" v-for="group in educationTree_educationGroup" :key="group.Id">
                <q-checkbox v-model="group.IsChecked" :label="group.Name"/>
              </div>
            </div>
          </fieldset>
        </q-slide-transition>

        <!-- <div
          class="col-12"
          v-for="group in educationGroupList.filter(x => x.IsChecked)"
          :key="group.Id"
        >
          <q-slide-transition>
            <fieldset class="col-12">
              <legend>زیر گروههای آموزشی</legend>
              <div class="row">
                <div
                  class="inline col-sm-4"
                  style="padding-top:5px;"
                  v-for="subGroup in group.SubGroups"
                  :key="subGroup.EducationSubGroupId"
                >
                  <div style="margin-top:20px;">{{subGroup.Name}}:</div>
                  <q-input
                    type="number"
                    style="margin-right:10px; width:20%;"
                    v-model="subGroup.Rate"
                    float-label="ضریب"
                  ></q-input>
                </div>
              </div>
            </fieldset>
          </q-slide-transition>
        </div>-->

        <q-slide-transition v-for="group in checkedEducationGroup" :key="group.Id">
          <fieldset class="col-12" v-if="checkedEducationGroup.length > 0">
            <legend>
              زیر گروههای آموزشی
              <span class="text-red">{{group.Name}}</span>
            </legend>
            <section class="row gutter-xs">
              <div
                class="col-md-2 col-sm-3 col-xs-6"
                v-for="subGroup in group.EducationSubGroup"
                :key="subGroup.Id"
              >
                <p class="shadow-1 q-pa-sm">
                  <label class="text-red">{{subGroup.Name}}:</label>
                  <q-input
                    type="number"
                    @focus="$event.target.select()"
                    v-model="subGroup.Rate"
                    float-label="ضریب"
                  ></q-input>
                </p>
              </div>
            </section>
          </fieldset>
        </q-slide-transition>
      </section>
    </div>
  </base-modal-edit>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import { vxm } from "src/store";
import { lessonValidations } from "src/validations/lessonValidation";
import { EducationTreeState } from "../../utilities/enumeration";

@Component({
  validations: lessonValidations
})
export default class LessonEditVue extends Vue {
  $v: any;

  //#region ### props ###
  @Prop({ type: Array, required: true }) expandedTreeIdsProp;
  //#endregion

  //#region ### data ###
  lessonStore = vxm.lessonStore;
  educationTreeStore = vxm.educationTreeStore;
  educationSubGroupStore = vxm.educationSubGroupStore;
  lookupStore = vxm.lookupStore;
  lesson = vxm.lessonStore.lesson;
  expandedTreeIds: Array<number> = [];
  educationGroup: Array<{
    Id: number;
    Name: string;
    IsChecked: boolean;
    EducationSubGroup: Array<{ Id: number; Name: string; Rate: number }>;
  }> = [];
  //#endregion

  //#region ### computed ###
  get educationTree_educationGroup() {
    this.educationGroup = this.educationTreeStore.gridData
      .filter(
        x =>
          x.Lookup_EducationTreeState != undefined &&
          x.Lookup_EducationTreeState.Name == "EducationTreeState" &&
          (x.Lookup_EducationTreeState.State ==
            EducationTreeState.EducationGroup ||
            x.Lookup_EducationTreeState.State ==
              EducationTreeState.GradeLevel) &&
          this.tickedNodeEducationTreeIds.some(y => y == x.Id)
      )
      .map(x => ({
        Id: x.Id,
        Name: x.Name,
        IsChecked: false,
        EducationSubGroup: this.subGroupByEducationGroupId(x.Id)
      }));
    return this.educationGroup;
  }

  get checkedEducationGroup() {
    return this.educationGroup.filter(x => x.IsChecked);
  }

  get subGroupByEducationGroupId() {
    return educationGroupId =>
      this.educationSubGroupStore.gridData
        .filter(x => x.EducationTreeId == educationGroupId)
        .map(x => ({
          Id: x.Id,
          Name: x.Name,
          Rate: 0
        }));
  }

  get tickedNodeEducationTreeIds() {
    if (!this.$refs.educationTree) return [];
    var isTicked = this.$refs.educationTree["isTicked"];

    var list: Array<number> = [];
    this.educationTreeStore.gridData.forEach(x => {
      if (isTicked(x.Id)) {
        list.push(x.Id);
      }
    });
    return list;
  }
  //#endregion

  //#region ### methods ###
  open() {
    this.expandedTreeIds = this.expandedTreeIdsProp;
    this.lookupStore.fillTopicNezam();
    this.educationTreeStore.fillList();
    this.educationSubGroupStore.fillList().then(() => {
      // fill ratios form server
      this.lesson.Ratios.forEach(ratio => {
        if (ratio.EducationSubGroup) {
          var educationTreeId = ratio.EducationSubGroup.EducationTreeId;
          this.educationGroup.forEach(group => {
            if (educationTreeId == group.Id && !group.IsChecked) {
              group.IsChecked = true;
            }
            group.EducationSubGroup.forEach(subGroup => {
              if (subGroup.Id == ratio.EducationSubGroupId) {
                subGroup.Rate = ratio.Rate;
              }
            });
          });
        }
      });
    });
  }

  submit(closeModal) {
    this.lessonStore.submitEdit({
      closeModal,
      educationGroup: this.educationGroup
    });
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.lessonStore.SET_EDIT_VUE(this);
  }
  //#endregion
}
// import viewModel from "viewModels/lessonViewModel";
// import { mapState, mapActions } from "vuex";

// export default {
//   data() {
//     return {
//       selectedNodeIds: null
//     };
//   },
//   /**
//    * methods
//    */
//   methods: {
//     ...mapActions("lessonStore", [
//       "toggleModalEditStore",
//       "editVueStore",
//       "submitEditStore",
//       "resetEditStore",
//       "getAllEduGroupAndEduSubGroupStore",
//       "setEducationGroupListLesson"
//     ]),
//     ...mapActions("lookupStore", ["fillTopicNezamStore"]),
//     ...mapActions("gradeStore", {
//       fillGradeDdlStore: "fillDdlStore"
//     }),
//     ...mapActions("gradeLevelStore", {
//       fillGradeLevel: "fillDdlStore"
//     }),
//     ...mapActions("educationTreeStore", [
//       "getEducationGroupsUnderSelectedList"
//     ]),
//     modalOpen: function() {
//       // this.getAllEduGroupAndEduSubGroupStore();
//       this.fillTopicNezamStore();
//       // this.fillGradeDdlStore();
//       // this.fillGradeLevel();
//     },
//     submitEdit: function() {
//       this.setEducationGroupListLesson(this.educationGroupList);
//       this.submitEditStore();
//     }
//   },
//   /**
//    * computed
//    */
//   computed: {
//     ...mapState("educationTreeStore", {
//       educationGroupList: "educationGroupList",
//       educationTreeData: "educationTreeData"
//     }),
//     ...mapState("lessonStore", {
//       modelName: "modelName",
//       lesson: "lesson",
//       isOpenModalEdit: "isOpenModalEdit",
//       EducationGroups: "EducationGroups",
//       educationGroupListLesson: "educationGroupList"
//       // selectedNodeIds: "selectedNodeIds"
//     }),
//     ...mapState("lookupStore", ["lookupTopicNezamDdl"]),
//     ...mapState("gradeStore", { gradeDdl: "gradeDdl" }),
//     ...mapState("gradeLevelStore", { gradeLevelDdl: "gradeLevelDdl" }),
//     gradeLavelFilteredDdl: function() {
//       return this.lesson.GradeId == 0
//         ? []
//         : this.gradeLevelDdl.filter(x => x.gradeId == this.lesson.GradeId);
//     }
//   },
//   /**
//    * validations
//    */
//   validations: viewModel,
//   /**
//    * created
//    */
//   created() {
//     this.editVueStore(this);
//   },
//   watch: {
//     "lesson.EducationTreeIds"(newVal) {
//       var eduGroupsId = [];
//       this.lesson.Ratios.forEach(element => {
//         var eduGroupId = element.EducationSubGroup.EducationTreeId;
//         if (!eduGroupsId.includes(eduGroupId)) eduGroupsId.push(eduGroupId);
//       });
//       if (this.isOpenModalEdit)
//         this.getEducationGroupsUnderSelectedList({
//           lstSelected: newVal,
//           isEdit: true,
//           eduGroupsId: eduGroupsId,
//           Ratios: this.lesson.Ratios
//         });
//     }
//   }
// };
</script>

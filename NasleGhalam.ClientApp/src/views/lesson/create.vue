<template>
  <base-modal-create
    :title="lessonStore.modelName"
    size="lg"
    :show="lessonStore.openModal.create"
    @confirm="submit"
    @reset="lessonStore.resetCreate"
    @close="lessonStore.OPEN_MODAL_CREATE(false)"
    @open="open"
  >
    <div class="col-md-4">
      <q-tree
        :nodes="educationTreeData"
        color="blue"
        :expanded.sync="expandedTreeIds"
        :ticked.sync="lesson.EducationTreeIds"
        tick-strategy="leaf"
        node-key="Id"
        ref="educationTree"
      />
    </div>
    <div class="col-md-8">
      <section class="row gutter-sm">
        <base-input :model="$v.lesson.Name" class="col-md-6"/>
        <base-select
          :model="$v.lesson.LookupId_Nezam"
          :options="lookupStore.topicNezamDdl"
          class="col-md-6"
        />
        <base-input :model="$v.lesson.NumberOfJudges" class="col-md-6"/>
        <base-field class="col-md-6" :model="$v.lesson.IsMain">
          <template slot-scope="data">
            <q-radio v-model="data.obj.$model" :val="false" label="خیر"/>
            <q-radio v-model="data.obj.$model" :val="true" label="بلی"/>
          </template>
        </base-field>
        <base-select
          :model="$v.lesson.LessonDepartmentId"
          :options="lessonDepartmentStore.ddl"
          class="col-md-6"
        />

        <q-slide-transition>
          <fieldset class="col-12" v-show="educationGroupByTickedEducationTreeIds.length > 0">
            <legend>گروههای آموزشی</legend>
            <section class="row">
              <div
                class="col-4"
                v-for="group in educationGroupByTickedEducationTreeIds"
                :key="group.Id"
              >
                <q-checkbox v-model="group.IsChecked" :label="group.Name"/>
              </div>
            </section>
          </fieldset>
        </q-slide-transition>

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
  </base-modal-create>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import { vxm } from "src/store";
import { lessonValidations } from "src/validations/lessonValidation";
import { EducationTreeState } from "../../utilities/enumeration";
import util from "src/utilities";

@Component({
  validations: lessonValidations
})
export default class LessonCreateVue extends Vue {
  $v: any;

  //#region ### props ###
  @Prop({ required: true }) educationTreeIdProp;
  @Prop({ type: Array, required: true }) expandedTreeIdsProp;
  //#endregion

  //#region ### data ###
  lessonStore = vxm.lessonStore;
  educationTreeStore = vxm.educationTreeStore;
  educationSubGroupStore = vxm.educationSubGroupStore;
  lessonDepartmentStore = vxm.lessonDepartmentStore;
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
  get educationTreeData() {
    return this.educationTreeStore.treeDataByEducationTreeId(
      this.educationTreeIdProp
    );
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

  get educationGroupByTickedEducationTreeIds() {
    // reset education group
    this.educationGroup.forEach(group => {
      if (this.tickedNodeEducationTreeIds.indexOf(group.Id) == -1) {
        group.IsChecked = false;
      }
      if (group.IsChecked == false) {
        group.EducationSubGroup.forEach(subGroup => {
          subGroup.Rate = 0;
        });
      }
    });
    return this.educationGroup.filter(x =>
      this.tickedNodeEducationTreeIds.some(y => y == x.Id)
    );
  }

  get tickedNodeEducationTreeIds() {
    if (!this.$refs.educationTree) return [];
    var getNodeByKey = this.$refs.educationTree["getNodeByKey"];
    var tickedNodeList: Array<any> = [];
    // ذخیره نود های برگ ها
    this.lesson.EducationTreeIds.forEach(id => {
      tickedNodeList.push(getNodeByKey(id));
    });

    var list: Array<number> = [];
    tickedNodeList.forEach(node => {
      while (node.parent != null) {
        if (list.indexOf(node.Id) == -1) {
          list.push(node.Id);
        }
        node = node.parent;
      }
    });
    return list;
  }
  //#endregion

  //#region ### methods ###
  open() {
    this.expandedTreeIds = this.expandedTreeIdsProp;
    this.lookupStore.fillTopicNezam();
    this.lessonDepartmentStore.fillList();
    this.educationTreeStore.fillList().then(() => {
      this.educationSubGroupStore.fillList().then(() => {
        this.fillEducationGroup();
      });
    });
  }

  submit(closeModal) {
    this.lessonStore.submitCreate({
      closeModal,
      educationGroup: this.educationGroup
    });
  }

  fillEducationGroup() {
    this.educationGroup = this.educationTreeStore.gridData
      .filter(
        x =>
          x.Lookup_EducationTreeState != undefined &&
          x.Lookup_EducationTreeState.Name == "EducationTreeState" &&
          x.Lookup_EducationTreeState.State == EducationTreeState.EducationGroup
      )
      .map(x => ({
        Id: x.Id,
        Name: x.Name,
        IsChecked: false,
        EducationSubGroup: this.subGroupByEducationGroupId(x.Id)
      }));
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.lessonStore.SET_CREATE_VUE(this);
  }
  //#endregion
}
</script>

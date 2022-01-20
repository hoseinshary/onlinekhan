<template>
  <section class="col-md-8 q-px-lg">
    <section slot="body" class="row gutter-sm">
 
      <div class="col-md-8">
        <base-btn-create
          v-if="canCreate"
          :label="`ایجاد (${lessonStore.modelName}) جدید`"
          @click="showModalCreate"
        />
        <br />
        <base-table
          :grid-data="lessonUserStore.lessonData"
          :columns="lessonGridColumn"
          hasIndex
        >
       
        </base-table>
      </div>
    </section>

    <!-- modals -->
    <modal-create
      v-if="canCreate"
      :educationTreeIdProp="educationTree.id"
      :expandedTreeIdsProp="educationTree.expanded"
    ></modal-create>
    <modal-edit v-if="canEdit" :expandedTreeIdsProp="educationTree.expanded"></modal-edit>
    <modal-delete v-if="canDelete"></modal-delete>
  </section>
</template>

<script lang="ts">
import { Vue, Component, Watch } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";
import { EducationTreeState } from "../../utilities/enumeration";

@Component({
  components: {
 
  }
})
export default class LessonVue extends Vue {
  //#region ### data ###
  lessonStore = vxm.lessonStore;
  lessonUserStore = vxm.lesson_UserStore;
  educationTreeStore = vxm.educationTreeStore;
  pageAccess = util.getAccess(this.lessonStore.modelName);
  lessonGridColumn = [
    {
      title: "نام",
      data: "Name"
    }
  ];
  educationTree = this.educationTreeStore.qTreeData;

  //#endregion

  //#region ### computed ###
  get canCreate() {
    return this.pageAccess.indexOf("ایجاد") > -1;
  }

  get canEdit() {
    return this.pageAccess.indexOf("ویرایش") > -1;
  }

  get isStudent() {
    return true;
  }

  get canDelete() {
    return this.pageAccess.indexOf("حذف") > -1;
  }

  get educationTree_GradeDdl() {
    return this.educationTreeStore.byStateDdl(EducationTreeState.Grade);
  }

  get educationTreeData() {
    return this.educationTreeStore.treeDataByEducationTreeId(
      this.educationTree.id
    );
  }
  //#endregion

  //#region ### watch ###
  
  //#endregion

  //#region ### methods ###




  //#endregion

  //#region ### hooks ###
  created() {
      

    this.lessonUserStore.fillMyLesson();
    
  }
  //#endregion
}
</script>


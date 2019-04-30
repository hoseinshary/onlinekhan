<template>
  <base-modal-edit
    :title="educationBookStore.modelName"
    :show="educationBookStore.openModal.edit"
    size="lg"
    @confirm="educationBookStore.submitEdit"
    @reset="educationBookStore.resetEdit"
    @open="open"
    @close="educationBookStore.OPEN_MODAL_EDIT(false)"
  >
    <section class="col-md-4">
      <q-input v-model="topicTree.filter" float-label="جستجو در مبحث" clearable/>
      <q-tree
        :nodes="topicTreeData"
        :expanded.sync="topicTree.expanded"
        :selected.sync="topicTree.selected"
        :ticked.sync="educationBook.TopicIds"
        :filter="topicTree.filter"
        tick-strategy="leaf"
        class="q-pt-lg"
        color="primary"
        accordion
        node-key="Id"
      />
    </section>
    <section class="col-md-8">
      <div class="row gutter-sm q-ma-sm q-px-sm shadow-1">
        <base-input :model="$v.educationBook.Name" class="col-md-6"/>
        <base-input :model="$v.educationBook.PublishYear" class="col-md-6"/>
        <base-field class="col-md-4" :model="$v.educationBook.IsExamSource">
          <template slot-scope="data">
            <q-toggle v-model="data.obj.$model"/>
          </template>
        </base-field>
        <base-field class="col-md-4" :model="$v.educationBook.IsActive">
          <template slot-scope="data">
            <q-toggle v-model="data.obj.$model"/>
          </template>
        </base-field>
        <base-field class="col-md-4" :model="$v.educationBook.IsChanged">
          <template slot-scope="data">
            <q-toggle v-model="data.obj.$model"/>
          </template>
        </base-field>
      </div>
    </section>
  </base-modal-edit>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { educationBookValidations } from "src/validations/EducationBookValidation";

@Component({
  validations: educationBookValidations
})
export default class EducationBookEditVue extends Vue {
  $v: any;

  //#region ### data ###
  educationBookStore = vxm.educationBookStore;
  educationBook = vxm.educationBookStore.educationBook;
  topicStore = vxm.topicStore;
  topicTree = this.topicStore.qTreeData;
  //#endregion

  //#region ### computed ###
  get topicTreeData() {
    var treeData = this.topicStore.treeDataByLessonId(
      this.educationBook.LessonId
    );
    if (this.topicTree.setToFirstLevel) {
      this.topicTree.expanded = this.topicTree.firstLevel;
      this.topicTree.setToFirstLevel = false;
    }
    return treeData;
  }
  //#endregion

  //#region ### methods ###
  open() {
    this.topicStore.fillList().then(() => {
      this.topicTree.expanded = this.topicTree.firstLevel;
    });
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.educationBookStore.SET_EDIT_VUE(this);
  }
  //#endregion
}
</script>


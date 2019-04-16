<template>
  <section class="col-12 q-px-sm">
    <!-- panel -->
    <base-panel>
      <span slot="title">{{topicStore.modelName}}</span>
      <div slot="body">
        <section class="row">
          <div class="col-sm-5">
            <section class="q-ma-sm q-pa-sm shadow-1">
              <q-select
                v-model="educationTree.id"
                :options="educationTree_GradeDdl"
                float-label="فیلتر درخت آموزش با مقطع"
                clearable
              />
              <q-tree
                :nodes="educationTreeData"
                :expanded.sync="educationTree.expandedIds"
                :ticked.sync="educationTree.tickedIds"
                class="tree-max-height"
                tick-strategy="leaf"
                color="blue"
                node-key="Id"
              />
              <q-select
                :value="lessonId"
                @change="lessonIdChanged"
                :options="lessonStore.ddl"
                float-label="انتخاب درس"
                class="q-pt-lg"
              />
            </section>
          </div>
          <div class="col-sm-7">
            <section class="q-ma-sm q-pa-sm shadow-1">
              <base-btn-create
                v-if="pageAccess.canCreate && lessonDdl.length && !topicTreeData.length && topicIndexObj.LessonId != 0"
                :label="`ایجاد (${modelName}) جدید`"
                @click="showModalCreate"
              />
              <q-input v-model="topicStore.treeVue.filter" float-label="جستجو در مبحث" clearable/>
              <q-tree
                :nodes="topicTreeData"
                :expanded.sync="topicStore.treeVue.expanded"
                :selected.sync="topicStore.treeVue.selected"
                :filter="topicStore.treeVue.filter"
                class="q-pt-lg"
                color="primary"
                accordion
                node-key="Id"
              >
                <div slot="header-custom" slot-scope="prop">
                  {{prop.node.label}}
                  <template>
                    <q-btn
                      v-if="canCreate"
                      outline
                      color="positive"
                      class="shadow-1 bg-white q-mr-sm q-px-xs"
                      icon="save"
                      size="sm"
                      @click.stop="showModalCreate(prop.node.Id, prop.node.label)"
                    />
                    <q-btn
                      v-if="canEdit"
                      outline
                      color="purple"
                      class="shadow-1 bg-white q-mr-sm q-px-xs"
                      icon="create"
                      size="sm"
                      @click.stop="showModalEdit(prop.node.Id, prop.node.label)"
                    />
                    <q-btn
                      v-if="canDelete && !(prop.node.children && prop.node.children.length)"
                      outline
                      color="red"
                      class="shadow-1 bg-white q-mr-sm q-px-xs"
                      icon="delete"
                      size="sm"
                      @click.stop="showModalDelete(prop.node.Id)"
                    />
                  </template>
                </div>
              </q-tree>
            </section>
          </div>
        </section>
      </div>
    </base-panel>

    <!-- modals -->
    <modal-create v-if="canCreate"></modal-create>
    <modal-edit v-if="canEdit"></modal-edit>
    <modal-delete v-if="canDelete"></modal-delete>
  </section>
</template>

<script lang="ts">
import { Vue, Component, Watch } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";
import { EducationTreeState } from "../../utilities/enumeration";
import ITopic, { DefaultTopic } from "../../models/ITopic";

@Component({
  components: {
    ModalCreate: () => import("./create.vue"),
    ModalEdit: () => import("./edit.vue"),
    ModalDelete: () => import("./delete.vue")
  }
})
export default class TopicVue extends Vue {
  //#region ### data ###
  topicStore = vxm.topicStore;
  topic = vxm.topicStore.topic;
  educationTreeStore = vxm.educationTreeStore;
  lessonStore = vxm.lessonStore;
  pageAccess = util.getAccess(this.topicStore.modelName);

  selected = null;
  lessonId = 0;
  educationTree: {
    id: number;
    expandedIds: Array<Object>;
    tickedIds: Array<number>;
  } = {
    id: 0,
    expandedIds: [],
    tickedIds: []
  };
  topicTree: {
    id: number;
    filter: string;
    tickedIds: Array<number>;
    setToFirstLevel: boolean;
  } = {
    id: 0,
    filter: "",
    tickedIds: [],
    setToFirstLevel: false
  };
  //#endregion

  //#region ### computed ###
  get canCreate() {
    return this.pageAccess.indexOf("ایجاد") > -1;
  }

  get canEdit() {
    return this.pageAccess.indexOf("ویرایش") > -1;
  }

  get canDelete() {
    return this.pageAccess.indexOf("حذف") > -1;
  }

  get educationTree_GradeDdl() {
    return this.educationTreeStore.byStateDdl(EducationTreeState.Grade);
  }

  get educationTreeData() {
    if (!this.educationTree.id) {
      return this.educationTreeStore.treeData;
    } else {
      return this.educationTreeStore.treeData[0].children.filter(
        x => x.Id == this.educationTree.id
      );
    }
  }

  get topicTreeData() {
    var treeData = this.topicStore.treeDataByLessonId(this.lessonId);
    if (this.topicTree.setToFirstLevel) {
      this.topicStore.treeVue.expanded = this.topicStore.treeVue.firstLevel;
      this.topicTree.setToFirstLevel = false;
    }
    return treeData;
  }
  //#endregion

  //#region ### watch ###
  @Watch("educationTree.id")
  educationTreeIdChanged(newVal, oldVal) {
    this.lessonId = 0;
    this.educationTree.tickedIds.splice(0, this.educationTree.tickedIds.length);

    let index = this.educationTree.expandedIds.indexOf(oldVal);
    if (index > -1) {
      this.educationTree.expandedIds.splice(index, 1);
    }

    if (this.educationTree.expandedIds.indexOf(newVal) == -1) {
      this.educationTree.expandedIds.push(newVal);
    }
  }

  @Watch("educationTree.tickedIds")
  educationTreeTickedIdsChanged(newVal) {
    this.lessonId = 0;
    this.lessonStore.fillListByEducationTreeIds(newVal);
  }
  //#endregion

  //#region ### methods ###
  showModalCreate(id, title) {
    this.topicStore.resetCreate();
    if (id && id > 0) {
      this.topic.ParentTopicId = id;
      this.topic.LessonId = this.lessonId;
      this.topic.ParentTopic = {
        Id: id,
        Title: title,
        ExamStock: 0,
        ExamStockSystem: 0,
        Importance: 0,
        IsExamSource: false,
        IsActive: true,
        LookupId_HardnessType: 0,
        LookupId_AreaType: 0,
        LessonId: 0
      };
    }
    this.topicStore.OPEN_MODAL_CREATE(true);
  }

  showModalEdit(id, title) {
    this.topicStore.resetEdit();
    this.topicStore.getById(id).then(() => {
      this.topicStore.OPEN_MODAL_EDIT(true);
    });
  }

  showModalDelete(id) {
    this.topicStore.getById(id).then(() => {
      this.topicStore.OPEN_MODAL_DELETE(true);
    });
  }

  lessonIdChanged(val) {
    this.lessonId = val;
    this.topicTree.setToFirstLevel = true;
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.topicStore.fillList();
    this.educationTreeStore.fillList().then(res => {
      this.educationTree.expandedIds = this.educationTreeStore.expandedTreeData;
    });
  }
  //#endregion
}
</script>


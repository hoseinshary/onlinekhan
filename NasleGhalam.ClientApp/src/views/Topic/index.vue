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
                :expanded.sync="educationTree.expanded"
                :ticked.sync="educationTree.leafTicked"
                :selected.sync="educationTree.selected"
                class="tree-max-height"
                tick-strategy="leaf"
                color="blue"
                node-key="Id"
              />
              <q-select
                :value="lessonId"
                @change="lessonIdChanged"
                :options="lessonStore.ddlByEducationTreeIds(educationTree.leafTicked)"
                float-label="انتخاب درس"
                class="q-pt-lg"
              />
            </section>
          </div>
          <div class="col-sm-7">
            <section class="q-ma-sm q-pa-sm shadow-1">
              <base-btn-create
                v-if="canCreate && lessonStore.ddl.length && !topicTreeData.length && lessonId != 0"
                :label="`ایجاد (${topicStore.modelName}) جدید`"
                @click="showModalCreate(0)"
              />
              <q-input v-model="topicTree.filter" float-label="جستجو در مبحث" clearable />
              <q-tree
                :nodes="topicTreeData"
                :expanded.sync="topicTree.expanded"
                :selected.sync="topicTree.selected"
                :filter="topicTree.filter"
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
            <q-btn class="q-mr-sm float-right" @click="printTopic" color="primary">
              چاپ مبحث
              <q-icon name="print" />
            </q-btn>
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
import router from "src/router";

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

  lessonId = 0;
  educationTree = this.educationTreeStore.qTreeData;
  topicTree = this.topicStore.qTreeData;

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
      this.topicTree.expanded = this.topicTree.firstLevel;
      this.topicTree.setToFirstLevel = false;
    }
    return treeData;
  }
  //#endregion

  //#region ### watch ###
  @Watch("educationTree.id")
  educationTreeIdChanged(newVal, oldVal) {
    this.lessonId = 0;
    this.educationTree.leafTicked.splice(
      0,
      this.educationTree.leafTicked.length
    );

    let index = this.educationTree.expanded.indexOf(oldVal);
    if (index > -1) {
      this.educationTree.expanded.splice(index, 1);
    }

    if (this.educationTree.expanded.indexOf(newVal) == -1) {
      this.educationTree.expanded.push(newVal);
    }
  }

  @Watch("educationTree.leafTicked")
  educationTreeTickedIdsChanged(newVal) {
    this.lessonId = 0;
  }
  //#endregion

  //#region ### methods ###
  showModalCreate(id, title) {
    this.topicStore.resetCreate();
    this.topic.ParentTopicId = id;
    this.topic.LessonId = this.lessonId;
    var defaultTopic = util.cloneObject(DefaultTopic);
    defaultTopic.Id = id;
    defaultTopic.Title = title;
    this.topic.ParentTopic = defaultTopic;
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

  printTopic() {
    this.topic.LessonId = this.lessonId;
    router.push("/topic/printTopic");
  }
  //#endregion

  //#region ### hooks ###
  mounted() {
    this.topicStore.fillList();
    this.lessonStore.fillList();
    this.educationTreeStore.fillList().then(res => {
      this.educationTree.expanded = this.educationTree.firstLevel;
    });
  }
  //#endregion
}
</script>


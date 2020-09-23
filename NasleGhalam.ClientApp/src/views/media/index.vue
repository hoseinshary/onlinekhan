<template>
  <section class="col-12 q-px-sm">
    <section class="row">
      <div class="col-md-4 col-lg-3">
    
        <!-- v-if="showElementTree('educationTree_GradeDdl')" -->
        <q-select
          v-model="educationTree.id"
          :options="educationTree_GradeDdl"
          float-label="فیلتر درخت آموزش با مقطع"
          clearable
          class="s-q-input-border s-spacing"
        />
        <!-- v-if="showElementTree('educationTreeData')" -->
        <q-tree
          :nodes="educationTreeData"
          :expanded.sync="educationTree.expanded"
          :ticked.sync="educationTree.leafTicked"
          tick-strategy="leaf"
          class="tree-max-height s-spacing s-border"
          color="blue"
          node-key="Id"
        />
        <q-select
          :value="lessonId"
          @change="lessonIdChanged"
          :options="lessonStore.ddlByEducationTreeIds(educationTree.leafTicked)"
          float-label="انتخاب درس"
          class="s-q-input-border s-spacing s-border"
        />
      </div>
      <div class="col-md-8 col-lg-9">
        <section class="s-border s-spacing row">
          <!-- v-show="!showWithoutTopic" -->
          <q-input
            v-model="topicTree.filter"
            float-label="جستجوی مبحث"
            clearable
            class="col-md-5 s-q-input-border"
          />
          <!-- v-show="!showWithoutTopic" -->
          <q-tree
            :nodes="topicStore.treeDataByLessonId2"
            :expanded.sync="topicTree.expanded"
            :selected.sync="topicTree.selected"
            :ticked.sync="topicTree.leafTicked"
            :filter="topicTree.filter"
            tick-strategy="leaf"
            class="q-py-lg col-12"
            color="primary"
            accordion
            node-key="Id"
          />

          <hr class="col-12 q-mb-sm" />

          <base-btn-create
            v-if="canCreate"
            :label="`ایجاد (${mediaStore.modelName}) جدید`"
            @click="showModalCreate"
            class="s-spacing"
          />

          <div class="col-12">
            <base-table :grid-data="mediaStore.gridData" :columns="mediaGridColumn" hasIndex>
              <template slot="Context" slot-scope="data">
                <div v-if="data.row.Context && data.row.Context.length> 100">
                  {{(`${data.row.Context.substring(0,100)} ...`)}}
                  <q-tooltip>{{data.row.Context}}</q-tooltip>
                </div>
                <div v-else>{{data.row.Context}}</div>
              </template>
              <template slot="Id" slot-scope="data">
                <a
                  :href="data.row.MediaPath"
                  target="_blank"
                  class="q-mr-sm"
                >فایل </a>
             
                <base-btn-edit v-if="canEdit" round @click="showModalEdit(data.row.Id)" />
                <base-btn-delete v-if="canDelete" round @click="showModalDelete(data.row.Id)" />
              </template>
            </base-table>
          </div>
        </section>
      </div>
    </section>

    <!-- modals -->
    <modal-create
      v-if="canCreate"
      :topicTreeDataProp="topicTreeData"
      :topicTickedIdsProp="topicTree.leafTicked"
      :lessonIdProp="lessonId"
    ></modal-create>
    <modal-edit
      v-if="canEdit"
      :topicTreeDataProp="topicTreeData"
      :lessonIdProp="lessonId"
    ></modal-edit>
    <modal-delete v-if="canDelete"></modal-delete>
    <modal-media-judge
      v-if="canMediaJudge"
      :topicTreeDataProp="topicTreeData"
      :lessonIdProp="lessonId"
    ></modal-media-judge>
    <modal-media-answer v-if="canMediaAnswer" :lessonIdProp="lessonId"></modal-media-answer>
  </section>
</template>

<script lang="ts">
import { Vue, Component, Prop, Watch } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";
import { EducationTreeState } from "../../utilities/enumeration";
import utilities from "src/utilities";

@Component({
  components: {
    ModalCreate: () => import("./create.vue"),
    ModalEdit: () => import("./edit.vue"),
    ModalDelete: () => import("./delete.vue")
    
  }
})
export default class MediaVue extends Vue {
  //#region ### data ###

  mediaStore = vxm.mediaStore;
  topicStore = vxm.topicStore;
  educationTreeStore = vxm.educationTreeStore;
  lessonStore = vxm.lessonStore;
  
  pageAccess = util.getAccess(this.mediaStore.modelName);
  mediaGridColumn = [
    {
      title: "کد",
      data: "Code"
    },
    {
      title: "عنوان",
      data: "Title"
    },
    {
      title: "عملیات",
      data: "Id",
      searchable: false,
      sortable: false,
      visible:
        this.canEdit ||
        this.canDelete 
        
    }
  ];
  lessonId = 0;
  educationTree = this.educationTreeStore.qTreeData;
  topicTree = this.topicStore.qTreeData;
  showNoJudgement = false;
  showWithoutTopic = false;
  showJudged = false;
  showActived = false;

  
  treeAccessElement = {
    educationTreeData: {
      canEditAdminProp: true
    },
    educationTree_GradeDdl: {
      canEditAdminProp: true
    }
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

  get canMediaJudge() {
    return this.pageAccess.indexOf("مشاهده کارشناسی") > -1;
  }

  get canMediaAnswer() {
    return this.pageAccess.indexOf("مشاهده جواب سوال") > -1;
  }

  get educationTree_GradeDdl() {
    return this.educationTreeStore.byStateDdl(EducationTreeState.Grade);
  }

  get educationTreeData() {
    return this.educationTreeStore.treeDataByEducationTreeId(
      this.educationTree.id
    );
  }

  get topicTreeData() {
    // var treeData = this.topicStore.treeDataByLessonId(this.lessonId);
    // if (this.topicTree.setToFirstLevel) {
    //   this.topicTree.expanded = this.topicTree.firstLevel;
    //   this.topicTree.setToFirstLevel = false;
    // }
    // return treeData;
    return this.topicStore.treeDataByLessonId2;
  }

  get canShowAdminFilter() {
    return this.pageAccess.indexOf("مشاهده فیلتر ادمین") > -1;
  }

  get canShowImportFilter() {
    return this.pageAccess.indexOf("مشاهده فیلتر ورود") > -1;
  }

  get canShowExpertFilter() {
    return this.pageAccess.indexOf("مشاهده فیلتر کارشناس") > -1;
  }

  get canShowTree() {
    return this.pageAccess.indexOf("مشاهده درخت پایه") > -1;
  }

  get activeAccess() {
    if (this.canShowAdminFilter) {
      return "canEditAdminProp";
    } else if (this.canShowImportFilter) {
      return "canEditImportProp";
    } else {
      return "canEditExpertProp";
    }
  }

  
  get activeAccessTree() {
    if (this.canShowTree) {
      return "canEditAdminProp";
    } else {
      return "canEditExpertProp";
    }
  }
  //#endregion

  //#region ### watch ###
  @Watch("educationTree.id")
  educationTreeIdChanged(newVal, oldVal) {
    this.lessonId = 0;
    // clear educationTree leaf
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

  @Watch("topicTree.leafTicked")
  topicTreeTickedIdsChanged(newVal) {
    this.fillGrid();
  }
  //#endregion

  //#region ### methods ###



  showElementTree(elementName) {
    var elem = this.treeAccessElement[elementName];
    return elem && elem[this.activeAccessTree];
  }
  showModalCreate() {
    this.mediaStore.resetCreate();
    this.mediaStore.OPEN_MODAL_CREATE(true);
  }

  showModalEdit(id) {
    this.mediaStore.resetEdit();
    this.mediaStore.getById(id).then(() => {
      this.mediaStore.OPEN_MODAL_EDIT(true);
    });
  }

  showModalDelete(id) {
    this.mediaStore.getById(id).then(() => {
      this.mediaStore.OPEN_MODAL_DELETE(true);
    });
  }

  lessonIdChanged(val) {
    this.lessonId = val;

    this.topicStore.fillListByLessonId(val).then(() => {
      this.topicTree.setToFirstLevel = true;
      // clear topicTree leaf
      utilities.clearArray(this.topicTree.leafTicked);
    });
  }

  fillGrid() {
    this.mediaStore.fillList({
      lessonId: this.lessonId,
      topicIds: this.topicTree.leafTicked
      
    });
  }

  
  //#endregion

  //#region ### hooks ###
  created() {
    this.lessonStore.fillList();
    this.educationTreeStore.fillList().then((res) => {
      this.topicStore.fillList();
      this.educationTree.expanded = this.educationTree.firstLevel;
    });
  }

  mounted() {
    let route = this["$route"];
    if (route && route.params.id && route.params.lessonId) {
      // fill educationTree
      this.educationTreeStore
        .GetAllByLessonId(route.params.lessonId)
        .then((tickedEducationTreeIds) => {
          utilities.clearArray(this.educationTree.leafTicked);
          tickedEducationTreeIds.forEach((x) => {
            this.educationTree.leafTicked.push(x);
          });
        })
        .then(() => {
          this.lessonIdChanged(+route.params.lessonId);
        })
        .then(() => {
          this.mediaStore.getById(+route.params.id).then(() => {
            // open edit modal
            this.mediaStore.OPEN_MODAL_EDIT(true);
            // topic leaf and fill grid
            if (this.mediaStore.media.TopicIds) {
              this.mediaStore.media.TopicIds.forEach((x) => {
                this.topicTree.leafTicked.push(x);
              });
            }
          });
        });
    }
  }
  //#endregion
}
</script>


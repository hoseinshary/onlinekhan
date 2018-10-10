<template>
  <section class="col-md-8">
    <!-- panel -->
    <my-panel>
      <span slot="title">{{modelName}}</span>
      <div slot="body">

        <br>
        <div class="row gutter-sm">
          <my-select :model="$v.topicObj.EducationGroupId"
                     :options="educationGroupDdl"
                     class="col-md-3"
                     filter />

          <my-select :model="$v.topicObj.LookupId_Nezam"
                     :options="lookupTopicNezamDdl"
                     class="col-md-2"
                     filter
          />
          <my-select :model="$v.topicObj.GradeId"
                     :options="gradeDdl"
                     class="col-md-2"
                     filter
          />
          <my-select :model="$v.topicObj.GradeLevelId"
                     :options="gradeLavelFilteredDdl"
                     class="col-md-2"
                     filter
          />
          <my-select :model="$v.topicObj.EducationGroup_LessonId"
                     :options="lessonFilteredDdl"
                     class="col-md-3"
                     filter
                     @change="GetAllTreeStore($event);lessonId =topicObj.EducationGroup_LessonId;"
          />
        
          <q-slide-transition>

            <div class="col-md-12"
                 v-if="topicObj.EducationGroup_LessonId!=0">
              <div class="col-12"
                   style="margin:10px;">مبحث انتخابی : {{selectedTreePath}}</div>
              <q-slide-transition>
                <div class="col-12"
                     >
                  <my-btn-create label="مشاهده جزئیات"
                                 color='light'
                                 icon='list'
                                 @click="showModalDetails" 
                                 :disabled="selectedNodeId == null"
                                 />
                  <my-btn-create v-if="pageAccess.canCreate"
                                 label='ایجاد زیر مبحث جدید'
                                 @click="showModalCreate" 
                                 :disabled="treeLst.length == 0 ? false : selectedNodeId == null"/>
                  <my-btn-edit v-if="pageAccess.canEdit"
                                 :disabled="selectedNodeId == null"
                               @click="showModalEdit" />
                  <my-btn-delete v-if="pageAccess.canDelete"
                                 :disabled="selectedNodeId == null"
                                 @click="showModalDelete" />
                </div>
              </q-slide-transition>
            </div>
          </q-slide-transition>
          <q-slide-transition>
  <q-tree :nodes="treeLst"
                  class="col-md-12"
                  color="blue"
                  :selected.sync="selectedNodeId"
                  default-expand-all
                  node-key="Id"
                  ref="topicTree"  />
          </q-slide-transition>
        </div>
      </div>
    </my-panel>

    <!-- modals -->
    <modal-details></modal-details>
    <modal-create v-if="pageAccess.canCreate"></modal-create>
    <modal-edit v-if="pageAccess.canEdit"></modal-edit>
    <modal-delete v-if="pageAccess.canDelete"></modal-delete>
  </section>
</template>

<script>
import { mapState, mapActions } from 'vuex';
import viewModel from 'viewModels/topicViewModel';

export default {
  components: {
    'modal-details': () => import('./details'),
    'modal-create': () => import('./create'),
    'modal-edit': () => import('./edit'),
    'modal-delete': () => import('./delete')
  },
  /**
   * data
   */
  validations: viewModel,
  data() {
    var pageAccess = this.$util.initAccess('/topic');
    return {
      pageAccess,
      selectedNodeId: null,
      selectedTreePath: '',
      lessonId: 0
    };
  },
  /**
   * methods
   */
  methods: {
    ...mapActions('topicStore', [
      'toggleModalCreateStore',
      'toggleModalDetailsStore',
      'toggleModalEditStore',
      'toggleModalDeleteStore',
      'getByIdStore',
      'change',
      'change2',
      'GetAllTreeStore',
      'fillGridStore',
      'resetCreateStore',
      'resetEditStore',
      'setLessonIdQndParentIdStore'
    ]),
    ...mapActions('educationGroupStore', {
      fillEduGrpDdlStore: 'fillDdlStore'
    }),

    ...mapActions('lessonStore', { fillLessonDdlStore: 'fillDdlStore' }),
    ...mapActions('lookupStore', [
      'fillTopicNezamStore'
    ]),
    ...mapActions('gradeStore', {
      'fillGradeDdlStore':'fillDdlStore'}
    ),
    ...mapActions('gradeLevelStore', {
      'fillGradeLevel':'fillDdlStore'}
    ),
    showModalCreate() {
      // reset data on modal show
      this.resetCreateStore();
      // show modal
      this.toggleModalCreateStore(true);
      debugger
      this.setLessonIdQndParentIdStore({
        lessonid: this.lessonId,
        parentId: this.selectedNodeId
      });
    },
    showModalDetails() {
      // get data by id
      this.getByIdStore(this.selectedNodeId).then(() => {
        // show modal
        this.toggleModalDetailsStore(true);
      });
    },
    showModalEdit() {
      // reset data on modal show
      this.resetEditStore();
      // get data by id
      this.getByIdStore(this.selectedNodeId).then(() => {
        // show modal
        this.toggleModalEditStore(true);
        this.setLessonIdQndParentIdStore({
          lessonid: this.lessonId,
          parentId: this.selectedNodeId
        });
      });
    },
    showModalDelete() {
      // get data by id
      this.getByIdStore(this.selectedNodeId).then(() => {
        // show modal
        this.toggleModalDeleteStore(true);
      });
    }
  },
  computed: {
    ...mapState('topicStore', {
      modelName: 'modelName',
      topicObj: 'topicObj',
      treeLst: 'treeLst'
    }),
    ...mapState('educationGroupStore', {
      educationGroupDdl: 'educationGroupDdl'
    }),
    ...mapState('lessonStore', {
      lessonDdl: 'allObjDdl'
    }),
    ...mapState('lookupStore', [
      'lookupTopicNezamDdl'
    ]),
    ...mapState('gradeStore', {gradeDdl:'gradeDdl'}
    ),
    ...mapState('gradeLevelStore',
      {gradeLevelDdl:'gradeLevelDdl'}
    ),
    gradeLavelFilteredDdl: function() {
      return this.topicObj.GradeId == 0
        ? []
        : this.gradeLevelDdl.filter(
            x => x.gradeId == this.topicObj.GradeId
          );
    },
    lessonFilteredDdl: function() {
      debugger
      return this.topicObj.EducationGroupId == 0
        ? []
        : this.lessonDdl.filter(
            x => x.educationGroupId == this.topicObj.EducationGroupId 
            &&  
              (this.topicObj.LookupId_Nezam == 0 || this.topicObj.LookupId_Nezam == null ||  x.LookupId_Nezam == this.topicObj.LookupId_Nezam) 
            &&
              (this.topicObj.GradeLevelId == 0 || this.topicObj.GradeLevelId == null || x.GradeLevelId == this.topicObj.GradeLevelId) 
          );
    }
  },
  created() {
    this.fillEduGrpDdlStore();
    this.fillLessonDdlStore();
    this.fillTopicNezamStore();
    this.fillGradeDdlStore();
    this.fillGradeLevel();
  },
  watch: {
    treeLst:function(){
      setTimeout(() => {
      this.$refs.topicTree.expandAll();
        this.selectedNodeId = null;
      }, 500);
    },
    selectedNodeId: function(val) {
      if (val == null) this.selectedTreePath = '';
      else {
        var str = [];
        var founded = false;
        function findNested(obj, value) {
          str.push(obj.label);
          if (obj.Id === value) {
            founded = true;
          } else if (obj.children) {
            obj.children.forEach(child => {
              if (!founded) {
                var flag = findNested(child, value);
                if (!flag && !founded) {
                  str.pop();
                }
              }
            });
          }
        }
        var lst = findNested(this.treeLst[0], this.selectedNodeId);
        this.selectedTreePath = str.join(' => ');
      }
    }
  }
};
</script>



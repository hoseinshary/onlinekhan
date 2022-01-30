<template>
  <section class="col-md-12 q-px-lg main">
    <section slot="body" class="row">

      <div class="col-md-3 q-py-lg">
        
        <!-- <q-card>
          <q-tree
            :nodes="educationTreeData"
            :expanded.sync="educationTree.expanded"
            :ticked.sync="educationTree.leafTicked"
            tick-strategy="strict"
            color="blue"
            node-key="Id"
          />
        </q-card> -->

        <br>
        <q-card>
          <q-list>
              <q-collapsible 
              v-for="item in educationTreeData"
              :key="item.Id" 
              :label="item.label">
                <q-collapsible 
                v-for="subItem in item.children"
                :key="subItem.Id"
                :label="subItem.label">
                  <q-btn 
                  v-for="section in subItem.children"
                  :key="section.Id"
                  :label="section.label"
                  @click="pushId(section.Id)"
                  :class="ids.indexOf(section.Id) > -1 ? 'colorOrange':''"
                  >
                  </q-btn>
                </q-collapsible>
                
              </q-collapsible>

          </q-list>

        </q-card>
          <br />
        <q-card>
          <q-select
            v-model="educationTree.id"
            :options="educationTree_GradeDdl"
            float-label="جستجو درخت آموزش با مقطع"
            clearable
            @input="pushId(educationTree.id)"
          />
        </q-card>
      </div>

      <div class="col-md-9 q-px-lg">
        <!--          
          <base-btn-create
            v-if="canCreate"
            :label="`ایجاد (${lessonStore.modelName}) جدید`"
            @click="showModalCreate"
          /> -->
        <!-- <base-table
          :grid-data="lessonStore.gridDataByEducationTreeIds(educationTree.leafTicked)"
          :columns="lessonGridColumn"
          hasIndex
        >
          <template slot="Id" slot-scope="data">
            <base-btn-edit round v-if="canEdit" @click="showModalEdit(data.row.Id)" />
            <base-btn-delete round v-if="canDelete" @click="showModalDelete(data.row.Id)" />

            <q-btn class="q-ma-sm" size="sm" round color="purple" icon="shopping_cart" @click="buyLesson(data.row.Id)">
              خرید
              <q-tooltip>خرید</q-tooltip>
            </q-btn>
          </template>
        </base-table> -->

        <!-- ati -->

        <div class="row">

          <div class="col-md-4 qCard q-pa-lg"
            v-for="packet in gridDataPacket" :key="packet.Id">
            <q-card>
                <q-card-media overlay-position="top" class="">
                <img src="/assets/f1.svg">
              </q-card-media>
              <q-card-title >
                {{ packet.Name }}
                <q-rating slot="subtitle" :max="5" />
              </q-card-title>
              <q-card-main class="">
                <p class="text-faded">توضیحاتی در مورد بسته آموزشی فوق</p>
              </q-card-main>
              
              <q-card-separator />

              <q-card-actions class="row justify-between items-center">
                <div>
                  <p class="price"> 64،000 تومان </p>
                </div>
                  <q-btn size="md" class="qBtn" icon="shopping_cart" @click="buyLesson(data.row.Id)">
                     
                  </q-btn>
              </q-card-actions>

            </q-card>
          </div>

        </div>


        <!-- ati -->



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
    ModalCreate: () => import("./create.vue"),
    ModalEdit: () => import("./edit.vue"),
    ModalDelete: () => import("./delete.vue")
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
    },
    {
      title: "عملیات",
      data: "Id",
      searchable: false,
      sortable: false,
      visible: this.canEdit || this.canDelete || this.isStudent
    }
  ];
  educationTree = this.educationTreeStore.qTreeData;

  // ati
  ids:Array<number> = [];

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
    console.log('this.educationTreeStore.treeDataByEducationTreeId',this.educationTreeStore.treeDataByEducationTreeId(
      this.educationTree.id
    ))
    return this.educationTreeStore.treeDataByEducationTreeId(
      this.educationTree.id
    );
  }


  // ati
  get gridDataPacket(){
    console.log('this.ids',this.ids);
    // console.log('this.lessonStore.gridDataByEducationTreeIds',this.lessonStore.gridDataByEducationTreeIds(this.ids));
    return this.lessonStore.gridDataByEducationTreeIds(this.ids);
  }


  // ati
  //#endregion

  //#region ### watch ###
  @Watch("educationTree.id")
  educationTreeIdChanged(newVal, oldVal) {
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
  //#endregion

  //#region ### methods ###
  showModalCreate() {
    this.lessonStore.resetCreate();
    this.lessonStore.OPEN_MODAL_CREATE(true);
  }

  showModalEdit(id) {
    this.lessonStore.resetEdit();
    this.lessonStore.getById(id).then(() => {
      this.lessonStore.OPEN_MODAL_EDIT(true);
    });
  }

  showModalDelete(id) {
    this.lessonStore.getById(id).then(() => {
      this.lessonStore.OPEN_MODAL_DELETE(true);
    });
  }

  buyLesson(id){
    this.lessonUserStore.buyLesson(id);
  }

  // ati
  pushId(id): void{
    this.ids = [];
    // console.log('id',id);
    this.ids.push(id);
  }

  //#endregion

  //#region ### hooks ###
  created() {
        this.lessonUserStore.SET_BUY_LESSON_VUE(this);

    this.lessonStore.fillList();
    this.educationTreeStore.fillList().then(res => {
      this.educationTree.expanded = this.educationTree.firstLevel;
    });
  }
  //#endregion
}
</script>

<style scoped>
*{
  color: #3d348b;
}
.main{
  background-color: #fcfaf9;
  min-height: 100vh;
}
.qCard{
  /* padding:1rem; */
  height: 100%;
}
.qCard .q-card{
  background-color: white;
}
.qCard .q-card:hover{
  box-shadow: none;
  border: .15rem solid #F36F21;
}
.qBtn{
  /* border: .2rem solid #3d348b; */
  border: none;
  background-color: #3d348b;
  color: #48E5C2;
  padding: 0 1.4rem;
}
p.price{
  margin: 0;
}


.q-option{
  display: none;
}

.q-collapsible .q-btn{
  border: none;
  outline: none;
  box-shadow: none;
  display: block;
  width: 100%;
  margin: auto 1rem;
}
.q-btn.colorOrange{
  color: #F36F21;
}
</style>


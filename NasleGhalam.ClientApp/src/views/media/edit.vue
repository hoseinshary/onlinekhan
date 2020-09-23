<template>
  <base-modal-edit
    :title="mediaStore.modelName"
    :show="mediaStore.openModal.edit"
    size="xl"
    @confirm="mediaStore.submitEdit()"
    @reset="mediaStore.resetEdit"
    @close="close"
    @open="open"
  >
  

    <div class="col-12" v-if="showElement('TopicIds')">
      <p v-for="(elem, index) in concatTopicArray" :key="index">{{elem}}</p>
    </div>
    <div class="col-md-3 col-sm-6">
      <section  class="q-my-sm shadow-1">
        <q-input v-model="topicFilter" float-label="جستجوی مبحث" clearable />
        <q-tree
          :nodes="topicTreeDataProp"
          :filter="topicFilter"
          :ticked.sync="media.TopicIds"
          tick-strategy="leaf"
          color="primary"
          accordion
          node-key="Id"
          ref="topicTree"
        />
      </section>

 

      <q-slide-transition>
        <section
          v-if="showElement('LookupId_MediaType') && media.LookupId_MediaType==6"
          class="q-ma-sm q-pa-sm shadow-1"
        >
         

      
       
        </section>
      </q-slide-transition>
    </div>

    <div class="col-md-6 col-sm-12">
      <section class="row gutter-md">

        <base-input :model="$v.media.Title" class="col-md-9"/>
        <q-field class="col-sm-4" >
          <q-uploader
            url
            float-label="فایل رسانه"
            name="file"
            hide-upload-button
            ref="file"
            
          />
        </q-field>
      
        <base-select
          :model="$v.media.LookupId_MediaType"
          :options="lookupStore.mediaTypeDdl"
          class="col-md-4"
          filter
        />
   
        <base-select
          :model="$v.media.WriterId"
          :options="writerStore.ddl"
          class="col-md-4"
          filter
        />



       <base-input :model="$v.media.Price" class="col-md-3"/>
        <base-input
          :model="$v.media.Description"
          class="col-md-12"
        />
      </section>
    </div>

    <div class="col-md-3 col-sm-6" >
      <q-input v-model="topicFilter2" float-label="جستجوی مبحث 2" clearable />
      <q-tree
        :nodes="topicStore.treeDataByLessonIds(lessonStore.relatedLessonIds(lessonIdProp))"
        :ticked.sync="media.TopicIds"
        :filter="topicFilter2"
        tick-strategy="leaf"
        color="primary"
        accordion
        node-key="Id"
        ref="topicTree2"
      />
    </div>
  </base-modal-edit>
</template>

<script lang="ts">
import { Vue, Component, Prop, Watch } from "vue-property-decorator";
import { vxm } from "src/store";
import { mediaValidations } from "src/validations/mediaValidation";
import util from "src/utilities";

@Component({
  validations: mediaValidations
})
export default class MediaEditVue extends Vue {
  $v: any;

  //#region ### props ###
  @Prop({ type: Array, required: true }) topicTreeDataProp;
  
  @Prop({ type: Number, required: true }) lessonIdProp;
  //#endregion

  //#region ### data ###
  stringArrya = [];
  mediaStore = vxm.mediaStore;
  lookupStore = vxm.lookupStore;
  tagStore = vxm.tagStore;
  topicStore = vxm.topicStore;
  lessonStore = vxm.lessonStore;
  media = vxm.mediaStore.media;
  userStore = vxm.userStore;
  writerStore = vxm.writerStore;
  topicFilter = "";
  topicFilter2 = "";
  
  
  concatTopicArray: Array<string> = [];
  //#endregion

  //#region ### computed ###
  get answersDdl() {
    return [
      { value: 1, label: "1" },
      { value: 2, label: "2" },
      { value: 3, label: "3" },
      { value: 4, label: "4" }
    ];
  }

  get topicAnswerDdl() {
    return this.concatTopicArray.map(x => ({ value: x, label: x })).filter(x => x.value != "");
  }
  //#endregion

  //#region ### watch ###
  @Watch("media.TopicIds")
  mediaTopicIdsChanged(newVal) {
    var getNodeByKey = this.$refs["topicTree"]["getNodeByKey"];
    util.clearArray(this.concatTopicArray);
    var strArr: Array<string> = [];
    newVal.forEach(x => {
      strArr = [];
      var node = getNodeByKey(x);
      while (node) {
        strArr.unshift(node.label);
        node = node.parent;
      }
      this.concatTopicArray.push(strArr.join(" => "));
    });

    // topic 2
    getNodeByKey = this.$refs["topicTree2"]["getNodeByKey"];
    newVal.forEach(x => {
      strArr = [];
      var node = getNodeByKey(x);
      while (node) {
        strArr.unshift(node.label);
        node = node.parent;
      }
      this.concatTopicArray.push(strArr.join(" => "));
    });
  }
  //#endregion

  //#region ### methods ###
  showElement(elementName) {
    return true;
  }

  open() {
    this.tagStore.fillList();
    this.lookupStore.fillMediaType();
    
    this.writerStore.fillList();
  }

  close() {
    this.mediaStore.OPEN_MODAL_EDIT(false);

    var route = this["$route"];
    if (route && route.params.id) {
      var router = this["$router"];
      router.push("/media");
    }
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.mediaStore.SET_EDIT_VUE(this);
  }
  //#endregion
}
</script>


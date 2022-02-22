<template>
  <base-modal-create
    :title="mediaStore.modelName"
    :show="mediaStore.openModal.create"
    size="xl"
    @confirm="mediaStore.submitCreate"
    @reset="mediaStore.resetCreate"
    @close="mediaStore.OPEN_MODAL_CREATE(false)"
    @open="open"
  >
    <div class="col-sm-4">
      <section class="q-ma-sm q-pa-sm shadow-1">
        <q-input v-model="topicFilter" float-label="جستجوی مبحث" clearable />
        <q-tree
          :nodes="topicTreeDataProp"
          :filter="topicFilter"
          :ticked.sync="media.TopicIds"
          tick-strategy="leaf"
          color="primary"
          accordion
          ref="topicTree"
          node-key="Id"
        />
      </section>
    </div>

    <div class="col-sm-8 row gutter-md">
      <base-input :model="$v.media.Title" class="col-md-9" />

      <q-field class="col-sm-4">
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
      <q-field class="col-sm-4">
        <q-uploader
          url
          float-label="عکس کاور"
          name="CoverImage"
          auto-expand
          ref="CoverImage"
          extensions=".jpg"
        />
      </q-field>

      <base-input :model="$v.media.Price" class="col-md-4" />
      <base-select
        :model="$v.media.WriterId"
        :options="writerStore.ddl"
        class="col-md-4"
        filter

      />

    <div class="col-md-4" ></div>
      <base-input :model="$v.media.Length" class="col-md-4" />
      <base-input :model="$v.media.YearOfBook" class="col-md-4" />
      <base-input :model="$v.media.PagesOfBook" class="col-md-4" />
    

      <div class="col-12">
        <p v-for="(elem, index) in concatTopicArray" :key="index">{{ elem }}</p>
      </div>
      <base-input :model="$v.media.Description" class="col-12" />
    </div>
  </base-modal-create>
</template>

<script lang="ts">
import { Vue, Component, Prop, Watch } from "vue-property-decorator";
import { vxm } from "src/store";
import { mediaValidations } from "src/validations/mediaValidation";
import util from "src/utilities";

@Component({
  validations: mediaValidations
})
export default class MediaCreateVue extends Vue {
  $v: any;

  //#region ### props ###
  @Prop({ type: Array, required: true }) topicTreeDataProp;
  @Prop({ type: Array, required: true }) topicTickedIdsProp;
  //#endregion

  //#region ### data ###
  mediaStore = vxm.mediaStore;
  lookupStore = vxm.lookupStore;
  tagStore = vxm.tagStore;
  topicStore = vxm.topicStore;
  media = vxm.mediaStore.media;
  writerStore = vxm.writerStore;
  userStore = vxm.userStore;
  topicFilter = "";
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
    return this.concatTopicArray.map(x => ({ value: x, label: x }));
  }
  //#endregion

  //#region ### methods ###
  open() {
    this.media.TopicIds = this.topicTickedIdsProp;
    this.tagStore.fillList();
    this.lookupStore.fillMediaType();

    this.writerStore.fillList();
    this.userStore.fillListSupervisor();
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
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.mediaStore.SET_CREATE_VUE(this);
  }
  //#endregion
}
</script>


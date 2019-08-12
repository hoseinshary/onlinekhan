<template>
  <q-layout view="lHh Lpr lFf">
    <q-page-container>
      <br />
      <div class="row justify-center q-mt-lg">
        <section class="col-12 q-px-sm">
          <!-- panel -->
          <base-panel>
            <span slot="title">{{topicStore.modelName}}</span>
            <div slot="body">
              <section class="row">
                <q-tree
                  :nodes="topicTreeData"
                  :expanded.sync="topicExpandedData"
                  class="q-pt-lg"
                  color="primary"
                  node-key="Id"
                  ref="topicTree"
                ></q-tree>
              </section>
            </div>
          </base-panel>
        </section>
      </div>
    </q-page-container>
  </q-layout>
</template>

<script lang="ts">
import { Vue, Component, Watch } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";
import { EducationTreeState } from "../../utilities/enumeration";
import ITopic, { DefaultTopic } from "../../models/ITopic";

@Component({})
export default class PrintTopicVue extends Vue {
  //#region ### data ###
  topicStore = vxm.topicStore;
  topic = vxm.topicStore.topic;
  topicExpandedData: Array<number> = [];
  //#endregion

  //#region ### computed ###
  get topicTreeData() {
    var treeData = this.topicStore.treeDataByLessonId(this.topic.LessonId);
    return treeData;
  }
  //#endregion

  //#region ### hooks ###
  created() {
    this.topicExpandedData = this.topicStore.gridData.map(x => x.Id);
  }
  //#endregion
}
</script>


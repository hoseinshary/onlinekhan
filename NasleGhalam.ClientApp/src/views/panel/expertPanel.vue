<template>
    <section>

<q-card>
  <q-card-title>
    تعداد سوال های کارشناسی شده
  </q-card-title>
  <q-card-separator />
  <q-card-main>
    {{panelStore.panel.CountAllJudged}}
  </q-card-main>
</q-card>
         
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
export default class QuestionVue extends Vue {
  //#region ### data ###
    panelStore = vxm.panelStore;
  
  //#endregion

  //#region ### computed ###
  
  //#endregion

  //#region ### watch ###
  
  //#endregion

  //#region ### methods ###
  


  
  //#endregion

  //#region ### hooks ###
  created() {
    this.panelStore.getData();
  }

  
  //#endregion
}

</script>
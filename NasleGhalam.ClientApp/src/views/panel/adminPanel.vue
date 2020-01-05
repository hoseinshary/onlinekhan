<template>
  <section class="col-md-8 q-px-lg">
    <section slot="body" class="row gutter-sm">
      <div class>
        <div class>
          <q-card class>
            <q-card-title>تعداد کل سوالات وارد شده</q-card-title>
            <q-card-separator />
            <q-card-main>{{panelStore.panel.CountAllQuestions}}</q-card-main>

            <q-card-actions>
              <q-btn flat color="primary" label="نمایش سوالات" />
            </q-card-actions>
          </q-card>
        </div>
        <div class>
          <q-card class>
            <q-card-title>تعداد کل سوالات فعال</q-card-title>
            <q-card-separator />
            <q-card-main>{{panelStore.panel.CountAllActiveQuestions}}</q-card-main>

            <q-card-actions>
              <q-btn flat color="primary" label="جزییرات" />
            </q-card-actions>
          </q-card>
        </div>
        <div class>
          <q-card class>
            <q-card-title>تعداد کل کارشناسی های انجام شده</q-card-title>
            <q-card-separator />
            <q-card-main>{{panelStore.panel.CountAllJudges}}</q-card-main>

            <q-card-actions>
              <q-btn flat color="primary" label="جزییرات" />
            </q-card-actions>
          </q-card>
        </div>
        
      </div>
      <div class="col-md-5">
        <q-card inline class>
          <q-card-media>
            <img src="/assets/img/time-management-vector.jpg" class="img-original-width" />
          </q-card-media>
        </q-card>
      </div>

      <div class="col-md-3">
        <q-card inline style>
          <q-card-media>
            <img
              :src="$q.localStorage.get.item('ProfilePic')"
              class="profile-image"
              alt="profile picture"
            />
          </q-card-media>
          <q-card-actions>
            <div class="q-pa-sm">

            <br/>
            <div>
              <q-btn color="primary" label="ویرایش اطلاعات فردی" />
            </div>
            <br/>
            <div>
              <q-btn color="secondary" label="تعییر رمز عبور" />
            </div>
            <br/>
            <div>
              <q-btn color="warning" label="بارگذاری عکس پروفایل" />
            </div>
            <br/>
            </div>
          </q-card-actions>
        </q-card>
      </div>
    </section>
  </section>
</template>


<script lang="ts">
import { Vue, Component, Watch } from "vue-property-decorator";
import { vxm } from "src/store";
import util from "src/utilities";
import { EducationTreeState } from "../../utilities/enumeration";

@Component({
  components: {}
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
    this.panelStore.getDataAdmin();
  }

  //#endregion
}
</script>
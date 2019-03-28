<template>
  <q-layout view="lHh Lpr lFf" class="login-layout row items-center">
    <q-page-container class="col-12">
      <div class="row justify-center">
        <q-card class="col-lg-4 col-md-6 col-sm-8">
          <q-card-title class="text-center text-bold text-white shadow-10 card-header">آنلاین خوان</q-card-title>
          <q-card-separator/>
          <q-card-main>
            <base-input :model="$v.loginUser.Username" class="text-bold"/>
            <base-input
              :model="$v.loginUser.Password"
              type="password"
              class="text-bold"
              @keyup.enter="login"
            />
            <div class="text-center">
              <q-btn color="light-blue-10" class="q-mr-sm" outline @click="login">
                ورود
                <q-icon name="keyboard_backspace"></q-icon>
              </q-btn>
            </div>
          </q-card-main>
        </q-card>
      </div>
    </q-page-container>
  </q-layout>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
import { userLoginValidations } from "src/validations/user/userLoginValidation";
@Component({
  validations: userLoginValidations
})
export default class UserLoginVue extends Vue {
  $v: any;

  //### data ###
  userStore = vxm.userStore;
  loginUser = vxm.userStore.loginUser;
  //--------------------------------------------------

  //### methods ###
  login() {
    this.userStore.login(this);
  }
  //--------------------------------------------------
}
</script>

<style>
.login-layout {
  background-image: url("../../assets/img/bg.jpg");
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;
}

.login-layout .q-card {
  opacity: 0.92;
  background-color: #34495e;
  border-radius: 10px;
  border: 1px solid #111866;
}
.login-layout .q-card .card-header {
  background-color: #111866;
}

.login-layout .q-card .q-card-main {
  background-color: white;
}
</style>

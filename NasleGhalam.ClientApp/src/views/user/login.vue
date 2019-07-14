<template>
  <q-layout view="lHh Lpr lFf" class="login-layout row items-center">
    <q-page-container class="col-12">
      <section class="row">
        <div class="col-md-7 main-pic"></div>
        <div class="col-md-5 main-input q-pa-lg">
          <section class="row justify-start content-center">
            <div class="logo"></div>
            <div class="col-xl-8 col-lg-9 col-md-10 col-sm-12 q-py-lg">
              <q-input
                v-model="loginUser.Username1"
                placeholder="نام کاربری"
                color="white"
                :before="[
                  {
                    icon: 'person',
                  }
                ]"
              />
            </div>
            <div class="col-xl-8 col-lg-9 col-md-10 col-sm-12 q-pb-lg">
              <q-input
                v-model="loginUser.Password1"
                type="password"
                placeholder="رمز عبور"
                color="white"
                :before="[
                  {
                    icon: 'lock',
                  }
                ]"
              />
            </div>
            <!-- <base-input
              :model="$v.loginUser.Username1"
              class="text-bold q-title col-xl-8 col-lg-9 col-md-10 col-sm-12"
              :before="[
                {
                  icon: 'person',
                }
              ]"
            />
            <base-input
              :model="$v.loginUser.Password1"
              type="password"
              class="text-bold q-title col-xl-8 col-lg-9 col-md-10 col-sm-12"
              @keyup.enter="login"
              :before="[
                {
                  icon: 'lock',
                }
              ]"
            />-->
            <div class="col-12">
              <button class="q-mr-sm" @click="login">
                ورود
                <q-icon name="keyboard_backspace"></q-icon>
              </button>
            </div>
          </section>
        </div>
        <div class="col-12 footer-pic">
          <span></span>
        </div>
      </section>
      <!-- <div class="row justify-center">
        <q-card class="col-lg-4 col-md-6 col-sm-8">
          <q-card-title class="text-center text-bold text-white shadow-10 card-header">آنلاین خوان</q-card-title>
          <q-card-separator/>
          <q-card-main>
            <base-input :model="$v.loginUser.Username1" class="text-bold"/>
            <base-input
              :model="$v.loginUser.Password1"
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
      </div>-->
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
  background-color: #034da2;
}

.login-layout .main-input {
  height: 80vh;
}
.login-layout .main-input .logo {
  background-image: url("../../assets/img/logo2.png");
  background-repeat: no-repeat;
  height: 177px;
  width: 477px;
}

.login-layout .main-input > section {
  height: 100%;
}

.login-layout .main-input .q-input {
  background-color: white;
  height: 80px;
  border-radius: 20px;
  padding-bottom: 27px;
}

.login-layout .main-input .q-input::after,
.login-layout .main-input .q-input::before {
  content: none;
}

.login-layout .main-input input {
  height: 30px;
  font-weight: bold;
  font-size: 25px;
}

.login-layout .main-input button {
  height: 80px;
  width: 170px;
  border-radius: 20px;
  border: none;
  font-weight: bold;
  font-size: 25px;
  background-color: #ee6c1e;
  color: #fffbf7;
}

.login-layout .main-pic {
  background-image: url("../../assets/img/login_main.png");
  background-repeat: no-repeat;
  /* background-position: center; */
  /* background-size: cover; */
  height: 731px;
  width: 732px;
}

.login-layout .footer-pic {
  background-color: #0072bb;
  height: 20vh;
  position: relative;
}

.login-layout .footer-pic span {
  width: 100px;
  height: 100px;
  position: absolute;
  right: calc(50% - 50px);
  border-radius: 50px;
  top: -45px;
  border: none;
  background-color: #0072bb;
}
/* .login-layout {
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
} */
</style>

<template>
  <q-layout view="lHh Lpr lFf" class="login-layout row items-center">
    <q-page-container class="col-12">
      <section class="row">
        <div class="col-md-7 main-pic gt-md"></div>
        <div class="col-lg-5 main-input q-pa-sm">
          <section class="row justify-center content-center">
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
                @keyup.enter="login"
                :before="[
                  {
                    icon: 'lock',
                  }
                ]"
              />
            </div>
            <div class="col-12 text-center">
              <button class="q-ma-sm btn btn-login" @click="login">
                ورود
                <q-icon name="keyboard_backspace"></q-icon>
              </button>
              <router-link to="/resume/registration" class="q-ma-sm btn btn-contribute">همکاری با ما</router-link>
            </div>
          </section>
        </div>
        <div class="col-12 footer-pic gt-md">
          <span></span>
        </div>
      </section>
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
  background-size: contain;
  height: 177px;
  max-width: 100%;
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

.login-layout .main-input .btn {
  height: 80px;
  width: 170px;
  border-radius: 20px;
  border: none;
  font-weight: bold;
  font-size: 25px;
  display: inline-block;
}

.btn-login {
  background-color: #ee6c1e;
  color: #fffbf7;
}

.btn-contribute {
  background-color: green;
  color: #fffbf7;
  padding: 21px;
  text-decoration: none;
}

.login-layout .main-pic {
  background-image: url("../../assets/img/login_main.png");
  background-repeat: no-repeat;
  /* background-position: center; */
  background-size: contain;
  height: 731px;
  width: 732px;
  max-width: 100%;
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
</style>

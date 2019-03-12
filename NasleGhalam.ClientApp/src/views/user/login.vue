<template>
  <!-- <div class="login">
    <div class="limiter">
      <div class="container-login100"
           style="background-image: url('../../assets/img/bg.jpg');">
        <transition appear
                    enter-active-class="animated fadeIn"
                    leave-active-class="animated fadeOutUp">
          <div id="container">
            <div class="wrap-login100">
              <div class="login100-form validate-form">
                <span class="login100-form-title p-b-34 p-t-27">
                  آموزشگاه کنکور نسل قلم
                </span>

                <span class="login100-form-logo">
                  <i class="zmdi zmdi-landscape"></i>
                </span>

                <span class="login200-form-title p-b-34 p-t-27">
                  ورود به سامانه
                </span>
                <my-input :model="$v.loginObj.UserName"
                          class="col-md-6" />
                <my-input :model="$v.loginObj.Password"
                          type="password"
                          class="col-md-6"
                          @keyup.enter="login" />

                <div class="container-login100-form-btn">
                  <button @click="login"
                          class="login100-form-btn"
                          style="padding-bottom: 5px;">
                    ورود
                  </button>
                </div>

                <div class="text-center forget">
                  <a class="txt1"
                     href="#">
                    فراموشی رمز عبور
                  </a>
                </div>
              </div>
            </div>
          </div>

        </transition>
      </div>
    </div>

  </div> -->
  <q-layout view="lHh Lpr lFf"
            class="login-layout">
    <q-page-container>
      <br>
      <br>
      <br>

      <div class="row justify-center items-center q-mt-lg">
        <q-card class="col-4">
          <q-card-title class="text-center text-bold text-white shadow-10 card-header">
            آنلاین خوان
          </q-card-title>
          <q-card-separator />
          <q-card-main>
            <my-input :model="$v.loginObj.UserName"
                      class="col-md-6 text-bold" />
            <my-input :model="$v.loginObj.Password"
                      type="password"
                      class="col-md-6 text-bold"
                      @keyup.enter="login" />

            <div class="text-center">

              <q-btn color="light-blue-10"
                     class="q-mr-sm"
                     outline
                     @click="login">
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

<script>
import viewModel from "viewModels/user/loginViewModel";
import { mapState, mapActions } from "vuex";

export default {
  validations: viewModel,

  methods: {
    ...mapActions("userStore", ["loginStore"]),
    login() {
      this.loginStore(this);
    }
  },
  computed: {
    ...mapState({
      loginObj: s => s.userStore.loginObj
    })
  }
};
</script>

<style>
.login-layout {
  background: url("../../assets/img/bg.jpg") no-repeat fixed center;
  opacity: 0.7;
}

.login-layout .q-card {
  opacity: 0.9;
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

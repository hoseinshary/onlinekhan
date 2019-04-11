<template>
  <q-layout view="lHh Lpr lFf"
            class="login-layout">
    <q-page-container class="q-pt-lg">
      <div class="row justify-center items-center q-mt-lg q-pa-lg">
        <q-card class="col-lg-4 col-md-6 col-sm-8">
          <q-card-title class="text-center text-bold text-white shadow-10 card-header">
            آنلاین خوان
          </q-card-title>
          <q-card-separator />
          <q-card-main>
            <my-input :model="$v.loginObj.UserName"
                      class="text-bold" />
            <my-input :model="$v.loginObj.Password"
                      type="password"
                      class="text-bold"
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

<template>
  <div class="login">
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

  </div>
</template>

<script>
import viewModel from 'viewModels/user/loginViewModel';
import { mapState, mapActions } from 'vuex';

export default {
  validations: viewModel,

  methods: {
    ...mapActions('userStore', ['loginStore']),
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

<style scoped>
* {
  margin: 0px;
  padding: 0px;
  box-sizing: border-box;
}

body,
html {
  height: 100%;
}

/*---------------------------------------------*/

/*---------------------------------------------*/
input {
  outline: none;
  border: none;
}

button {
  outline: none !important;
  border: none;
  background: transparent;
}

button:hover {
  cursor: pointer;
}

.limiter {
  width: 100%;
  margin: 0 auto;
}

.container-login100 {
  width: 100%;
  min-height: 100vh;
  display: -webkit-box;
  display: -webkit-flex;
  display: -moz-box;
  display: -ms-flexbox;
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  align-items: center;
  padding: 15px;

  background-repeat: no-repeat;
  background-position: center;
  background-size: cover;
  position: relative;
  z-index: 1;
}

.container-login100::before {
  content: '';
  display: block;
  position: absolute;
  z-index: -1;
  width: 100%;
  height: 100%;
  top: 0;
  left: 0;
  background-color: rgba(255, 255, 255, 0.9);
}

.wrap-login100 {
  width: 400px;
  border-radius: 10px;
  overflow: hidden;
  padding: 10px 20px 17px 20px;

  background: #9152f8;
  background: -webkit-linear-gradient(top, #7579ff, #b224ef);
  background: -o-linear-gradient(top, #7579ff, #b224ef);
  background: -moz-linear-gradient(top, #7579ff, #b224ef);
  background: linear-gradient(top, #7579ff, #b224ef);
}

/*------------------------------------------------------------------
[ Form ]*/

.login100-form {
  width: 100%;
}

.login100-form-logo {
  font-size: 60px;
  color: #333333;

  display: -webkit-box;
  display: -webkit-flex;
  display: -moz-box;
  display: -ms-flexbox;
  display: flex;
  justify-content: center;
  align-items: center;
  width: 120px;
  height: 120px;
  border-radius: 50%;
  background-color: #fff;
  margin: 0 auto;
}

.login100-form-title {
  font-size: 25px;
  color: #fff;
  line-height: 1.2;
  text-align: center;
  text-transform: uppercase;

  padding-bottom: 30px;
  padding-top: 10px;
  display: block;
}

.login200-form-title {
  font-size: 20px;
  color: #fff;
  line-height: 1.2;
  text-align: center;
  text-transform: uppercase;

  padding-bottom: 30px;
  padding-top: 30px;
  display: block;
}

/*------------------------------------------------------------------
[ Input ]*/
div >>> .q-if-label.ellipsis.full-width.absolute.self-start {
  color: wheat !important;
}
div >>> input {
  color: white;
}
div >>> .q-field-error.col {
  color: #5c1818ab;
}
/*------------------------------------------------------------------
[ Button ]*/
.container-login100-form-btn {
  width: 100%;
  display: -webkit-box;
  display: -webkit-flex;
  display: -moz-box;
  display: -ms-flexbox;
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
}

.login100-form-btn {
  font-size: 16px;
  color: #555555;
  line-height: 1.2;

  display: -webkit-box;
  display: -webkit-flex;
  display: -moz-box;
  display: -ms-flexbox;
  display: flex;
  justify-content: center;
  align-items: center;
  padding: 0 20px;
  min-width: 75px;
  height: 30px;
  border-radius: 25px;

  background: #9152f8;
  background: -webkit-linear-gradient(bottom, #7579ff, #b224ef);
  background: -o-linear-gradient(bottom, #7579ff, #b224ef);
  background: -moz-linear-gradient(bottom, #7579ff, #b224ef);
  background: linear-gradient(bottom, #7579ff, #b224ef);
  position: relative;
  z-index: 1;

  -webkit-transition: all 0.4s;
  -o-transition: all 0.4s;
  -moz-transition: all 0.4s;
  transition: all 0.4s;
}

.login100-form-btn::before {
  content: '';
  display: block;
  position: absolute;
  z-index: -1;
  width: 100%;
  height: 100%;
  border-radius: 25px;
  background-color: #fff;
  top: 0;
  left: 0;
  opacity: 1;

  -webkit-transition: all 0.4s;
  -o-transition: all 0.4s;
  -moz-transition: all 0.4s;
  transition: all 0.4s;
}

.login100-form-btn:hover {
  color: #fff;
}

.login100-form-btn:hover:before {
  opacity: 0;
}
a {
  color: white;
  text-decoration: none;
}
.forget {
  margin-top: 50px;
}
</style>



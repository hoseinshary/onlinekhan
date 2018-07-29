
<template>
    <div class="login">
        <!-- <loading></loading> -->
        <div class="limiter">
            <div class="container-login100"
                 style="background-image: url('../../assets/img/bg.jpg');">
                <!-- <transition appear enter-active-class="animated fadeInUp" leave-active-class="animated fadeOutUp">
        <div @click="doTrans" v-if="show" id="login-button">
          <img src="../../assets/img/login-w-icon.png">
        </div>
      </transition> -->
                <transition appear
                            enter-active-class="animated fadeIn"
                            leave-active-class="animated fadeOutUp">
                    <div v-if="show2"
                         id="container">
                        <div class="wrap-login100">
                            <form class="login100-form validate-form">
                                <span class="login100-form-title p-b-34 p-t-27">
                                    آموزشگاه کنکور نسل قلم
                                </span>

                                <span class="login100-form-logo">
                                    <i class="zmdi zmdi-landscape"></i>
                                </span>

                                <span class="login200-form-title p-b-34 p-t-27">
                                    ورود به سامانه
                                </span>
                                <my-input :model="$v.instanceObj.Username"
                                          class="col-md-6" />
                                <my-input :model="$v.instanceObj.Password"
                                          type="password"
                                          class="col-md-6" />

                                <div class="container-login100-form-btn">
                                    <button class="login100-form-btn"
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
                            </form>
                        </div>
                    </div>

                    <!-- <div v-if="show2" id="container">
        <h3 style="font-family:tahoma ; text-align:center ; font-weight: bold; background: #a6aab6c7; border-radius: 0px 5px;color:white; font-size:3vh;margin: 5px;">
          نسل قلم
                 </h3>
        <h4 style="font-family:tahoma ; text-align:center ; font-weight:bold; color:white; font-size:14px;margin: 5px;">
          ورود به سیستم
        </h4>
        <span class="close-btn">
          <img @click="returnTrans" src="../../assets/icons/close.png">
        </span>
        <form>

          <input @keyup.enter="checkAuth" v-model="username" type="text" name="username" placeholder="نام کاربری">
          <input @keyup.enter="checkAuth" v-model="password" type="password" name="pass" placeholder="رمز عبور">
          <a @click="checkAuth" style="font-family:tahoma ; cursor: pointer;   background: #3d91db; text-align:center ; font-weight:bold; color:white; font-size:14px;">ورود</a>
        </form>
      </div> -->
                </transition>
            </div>
        </div>

    </div>
</template>

<script>
// import _userService from "serviceLayers/UserService";
// import loading from "components/loading.vue";
import viewModel from 'viewModels/user/loginViewModel';
import { mapState } from 'vuex';

import { Notify } from 'quasar';
export default {
  validations: viewModel,
  data: () => ({
    show2: true,
    show: true,
    username: '',
    password: ''
  }),
  methods: {
    doTrans() {
      this.show = false;
      setTimeout(() => {
        this.show2 = true;
      }, 500);
    },
    returnTrans() {
      this.show2 = false;
      setTimeout(() => {
        this.show = true;
      }, 500);
    },
    checkAuth() {
      // _userService.logIn(this.username, this.password).then(response => {
      //   var log = response.data;
      //   Notify.create({
      //     message: log.Message,
      //     type: log.MessageType == 1 ? "positive" : "negative",
      //     position: "top",
      //     timeout: 1000
      //   });
      //   debugger;
      //   if (log.MessageType == 1) {
      //     this.$q.localStorage.set("Token", log.Token);
      //     _userService.getMenu().then(axiosData => {
      //       this.$q.localStorage.set(
      //         "authList",
      //         axiosData.data.map(x => x.EnName.toLowerCase())
      //       );
      //       this.$q.localStorage.set("menuList", axiosData.data);
      //       this.$router.push(log.DefaultPage);
      //     });
      //   }
      // });
      this.$q.localStorage.set('authList', ['/user', '/role']);
      this.$q.localStorage.set('menuList', '');
      this.$router.push('/user');
    }
  },
  computed: {
    ...mapState({
      instanceObj: s => s.userStore.instanceObj
    })
  },
  created: function() {
    this.doTrans();
  }
  // mounted: function() {
  //   $("body").addClass("myLogin");
  // },
  // destroyed: function() {
  //   $("body").removeClass("myLogin");
  // },
  //   components: { loading }
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



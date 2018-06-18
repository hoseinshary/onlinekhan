<template>
  <div class="login">
    <loading></loading>
    <transition appear enter-active-class="animated fadeInUp" leave-active-class="animated fadeOutUp">
      <div @click="doTrans" v-if="show" id="login-button">
        <img src="../../assets/img/login-w-icon.png">
      </div>
    </transition>
    <transition appear enter-active-class="animated fadeInUp" leave-active-class="animated fadeOutUp">
      <div v-if="show2" id="container">
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
      </div>
    </transition>

  </div>
</template>

<script>
import _userService from "serviceLayers/UserService";
import loading from "components/loading.vue";

import { Notify } from "quasar";
export default {
  data: () => ({
    show2: false,
    show: true,
    username: "",
    password: ""
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
      this.$q.localStorage.set("authList", ["/user", "/role"]);
       this.$q.localStorage.set("menuList", "");
      this.$router.push("/user");
    }
  },
  mounted: function() {
    $("body").addClass("myLogin");
  },
  destroyed: function() {
    $("body").removeClass("myLogin");
  },
  components: { loading }
};
</script>

<style>
body.myLogin {
  background: url(../../assets/img/bg.jpg) no-repeat center center fixed !important;
  -webkit-background-size: cover !important;
  -moz-background-size: cover !important;
  -o-background-size: cover !important;
  background-size: cover !important;
  overflow: hidden !important;
}
</style>
<style>
.login img {
  display: block;
  margin: auto;
  width: 100%;
  height: auto;
}

.login #login-button {
  cursor: pointer;
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  padding: 10px;
  margin: auto;
  width: 100px;
  height: 100px;
  border-radius: 50%;
  background: rgba(14, 31, 50, 0.08);
  overflow: hidden;
  /* opacity: 0.4; */
  box-shadow: 10px 10px 30px #000;
}

/* Login container */
.login #container {
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  margin: auto;
  width: 70%;
  height: 250px;
  border-radius: 5px;
  background: rgba(3, 3, 3, 0.3);
  box-shadow: 1px 1px 50px #000;
  /* display: none; */
}

.login .close-btn {
  position: absolute;
  cursor: pointer;
  font-family: "tahoma", sans-serif;
  line-height: 18px;
  top: 3px;
  right: 3px;
  width: 20px;
  height: 20px;
  text-align: center;
  border-radius: 10px;
  opacity: 0.2;
  -webkit-transition: all 2s ease-in-out;
  -moz-transition: all 2s ease-in-out;
  -o-transition: all 2s ease-in-out;
  transition: all 0.2s ease-in-out;
}

.login .close-btn:hover {
  opacity: 0.5;
}

/* Heading */

/* Inputs */
.login a,
.login input {
  direction: rtl;
  font-family: tahoma;
  text-decoration: none;
  position: relative;
  width: 60%;
  display: block;
  margin: 9px auto;
  font-size: 14px;
  font-weight: bold;
  color: #fff;
  padding: 8px;
  border-radius: 6px;
  border: none;
  /* background: white; */
  color: #ffd800;

  background: #03215f4d;
  -webkit-transition: all 2s ease-in-out;
  -moz-transition: all 2s ease-in-out;
  -o-transition: all 2s ease-in-out;
  transition: all 0.2s ease-in-out;
  /* cursor: pointer; */
}

.login input:focus {
  outline: none;
  box-shadow: 3px 3px 10px #333;
  background: rgba(3, 3, 3, 0.18);
}

/* Placeholders */
.login ::-webkit-input-placeholder {
  color: #ddd;
}
.login :-moz-placeholder {
  /* Firefox 18- */
  color: red;
}
.login ::-moz-placeholder {
  /* Firefox 19+ */
  color: red;
}
.login :-ms-input-placeholder {
  color: #333;
}
</style>



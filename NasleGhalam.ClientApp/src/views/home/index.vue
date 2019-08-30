<template>
  <q-layout view="lHh Lpr lFf" class="home">
    <q-layout-header reveal>
      <ul>
        <li>
          <a href="#" class="logo"></a>
        </li>
        <li>
          <a href="#">آزمون</a>
        </li>
        <li>
          <a href="#">آموزش</a>
        </li>
        <li>
          <a href="#">مشاوره</a>
        </li>
        <li>
          <a href="#">لیست قیمت</a>
        </li>
        <li>
          <a href="#">کلاس های آموزشی</a>
        </li>
      </ul>
      <ul>
        <li>
          <router-link to="/user/login">ورود</router-link>
        </li>
        <li>
          <a href="#">ثبت نام</a>
        </li>
      </ul>
    </q-layout-header>
    <q-page-container class="col-12">
      <section class="row">
        <div class="col-12 main-image-section">
          <img src="assets/img/home-main-img.png" />
        </div>

        <div class="col-12 introduce-section">
          <h3>سامانه آموزشی چگونه به شما کمک می کند؟</h3>
          <section class="row justify-center">
            <div class="col-sm-4">
              <div class="box">
                <img src="/assets/img/home-intelligent-consult.png" alt />
                <p>مشاوره هوشمند</p>
              </div>
              <div class="box">
                <img src="/assets/img/home-updated-question-bank.png" alt />
                <p>بانک سوالات به روز</p>
              </div>
              <div class="box">
                <img src="/assets/img/home-assay-judgment.png" alt />
                <p>تحلیل و مقایسه آزمون ها</p>
              </div>
            </div>
            <div class="col-sm-4">
              <div class="box">
                <img src="/assets/img/home-step-management.png" alt />
                <p>مدیریت کلیه مراحل آموزشی</p>
              </div>
              <div class="box">
                <img src="/assets/img/home-dedicate-page.png" alt />
                <p>صفحه اختصاصی برای هر کاربر</p>
              </div>
              <div class="box">
                <img src="/assets/img/home-online-assay.png" alt />
                <p>برگذاری آزمون آنلاین</p>
              </div>
            </div>
          </section>
        </div>

        <div class="col-12 counter-section">
          <section class="row">
            <div class="col-4 text-center">
              <div class="box">
                <img src="assets/img/counter-customer.png" />
                <span>4</span>
                <p>مشتری</p>
              </div>
            </div>
            <div class="col-4 text-center">
              <div class="box">
                <img src="assets/img/counter-assay.png" />
                <span>8</span>
                <p>آزمون</p>
              </div>
            </div>
            <div class="col-4 text-center">
              <div class="box">
                <img src="assets/img/counter-volunteer.png" />
                <span>33</span>
                <p>داوطلب</p>
              </div>
            </div>
          </section>
        </div>
      </section>
    </q-page-container>
  </q-layout>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
@Component({})
export default class UserLoginVue extends Vue {
  //### data ###
  //--------------------------------------------------
  //### methods ###
  //--------------------------------------------------
}
</script>

<style>
.home .q-layout-header {
  background-color: #fff;
  box-shadow: none;
}
.home .q-layout-header ul {
  display: inline-block;
  list-style-type: none;
  margin-top: 90px;
  padding: 0;
  overflow: hidden;
  background-color: white;
  height: 60px;
  font-size: 24px;
  font-weight: bold;
}

.home .q-layout-header ul:first-child {
  margin-left: 50px;
}

.home .q-layout-header ul:last-child {
  margin-right: 50px;
  float: right;
}

.home .q-layout-header ul li {
  float: left;
}

.home .q-layout-header ul li a {
  display: block;
  text-align: center;
  padding: 14px 16px;
  text-decoration: none;
  color: #464646;
}

.home .q-layout-header li .logo {
  background-image: url("../../assets/img/logo1.png");
  background-repeat: no-repeat;
  background-size: contain;
  height: 70px;
  max-width: 100%;
  width: 180px;
  margin-left: 50px !important;
}
/* --- */

/* main-image */
.home .main-image-section {
  margin-bottom: 70px;
}

.home .main-image-section img {
  height: 800px;
  max-width: 100%;
  width: 100%;
}
/* --- */

/* introduce */
.home .introduce-section {
  margin-bottom: 100px;
}
.home .introduce-section h3 {
  color: #464646;
  text-align: center;
  font-weight: bold;
  font-size: 40px;
}

.home .introduce-section .box {
  margin-top: 30px;
}

.home .introduce-section .box p {
  color: #0072bc;
  font-size: 40px;
  font-weight: bold;
  display: inline-block;
}

.home .introduce-section .box img {
  width: 65px;
  height: 65px;
  display: inline-block;
  margin-right: 10px;
}
/* --- */

/* counter */
.home .counter-section {
  /* height: 500px; */
  background-color: #72bf44;
}

.home .counter-section .box img {
  width: 100px;
  height: 100px;
  display: block;
  margin: 35px auto 50px auto;
}

.home .counter-section .box span {
  color: #004995;
  font-size: 80px;
  font-weight: bold;
  display: block;
  margin-bottom: 25px;
}

.home .counter-section .box p {
  color: white;
  font-size: 40px;
  font-weight: bold;
  display: block;
  margin-bottom: 70px;
}

/* --- */
</style>

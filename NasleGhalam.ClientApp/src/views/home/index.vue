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
          <router-link to="/user/register">ثبت نام</router-link>
        </li>
        <li>
          <router-link to="/resume/registration">همکاری با ما</router-link>
        </li>
      </ul>
    </q-layout-header>
    <q-page-container class="col-12">
      <section class="row">
        <div class="col-12 main-image-section">
          <img src="assets/img/home-main-img.png" alt="img" />
        </div>

        <div class="col-12 introduce-section">
          <h3>سامانه آموزشی چگونه به شما کمک می کند؟</h3>
          <section class="row justify-center">
            <div class="col-md-6 col-lg-5 col-sm-12 right-side">
              <div class="box">
                <img src="/assets/img/home-intelligent-consult.png" alt="مشاوره هوشمند" />
                <p>مشاوره هوشمند</p>
              </div>
              <div class="box">
                <img src="/assets/img/home-updated-question-bank.png" alt="بانک سوالات به روز" />
                <p>بانک سوالات به روز</p>
              </div>
              <div class="box">
                <img src="/assets/img/home-assay-judgment.png" alt="تحلیل و مقایسه آزمون ها" />
                <p>تحلیل و مقایسه آزمون ها</p>
              </div>
            </div>
            <div class="col-md-6 col-lg-5 col-sm-12">
              <div class="box">
                <img src="/assets/img/home-step-management.png" alt="مدیریت کلیه مراحل آموزشی" />
                <p>مدیریت کلیه مراحل آموزشی</p>
              </div>
              <div class="box">
                <img src="/assets/img/home-dedicate-page.png" alt="صفحه اختصاصی برای هر کاربر" />
                <p>صفحه اختصاصی برای هر کاربر</p>
              </div>
              <div class="box">
                <img src="/assets/img/home-online-assay.png" alt="برگذاری آزمون آنلاین" />
                <p>برگذاری آزمون آنلاین</p>
              </div>
            </div>
          </section>
        </div>

        <div class="col-12 counter-section">
          <section class="row">
            <div class="col-sm-4 text-center">
              <div class="box">
                <img src="assets/img/counter-customer.png" alt="مشتری" />
                <span>4</span>
                <p>مشتری</p>
              </div>
            </div>
            <div class="col-sm-4 text-center">
              <div class="box">
                <img src="assets/img/counter-assay.png" alt="آزمون" />
                <span>8</span>
                <p>آزمون</p>
              </div>
            </div>
            <div class="col-sm-4 text-center">
              <div class="box">
                <img src="assets/img/counter-volunteer.png" alt="داوطلب" />
                <span>33</span>
                <p>داوطلب</p>
              </div>
            </div>
          </section>
        </div>

        <div class="col-12 introduce2-section">
          <h3>سامانه آموزشی چگونه به شما کمک می کند؟</h3>
          <section class="row justify-center">
            <div class="col-lg-4 col-md-5 col-sm-12">
              <div class="box">
                <img src="/assets/img/introduce2-register-information.png" alt="ثبت اطلاعات" />
                <p>ثبت اطلاعات</p>
                <span></span>
              </div>
              <div class="box">
                <img src="/assets/img/introduce2-dedicate-page.png" alt="صفحه اختصاصی مشتری" />
                <p>صفحه اختصاصی مشتری</p>
                <span></span>
              </div>
              <div class="box">
                <img src="/assets/img/introduce2-delegate.png" alt="نمایندگی" />
                <p>نمایندگی</p>
                <span></span>
              </div>
              <div class="box">
                <img src="/assets/img/introduce2-question-judgment.png" alt="تحلیل سوال" />
                <p>تحلیل سوال</p>
                <span></span>
              </div>
              <div class="box">
                <img src="/assets/img/introduce2-prerequisite.png" alt="پیش نیاز" />
                <p>پیش نیاز</p>
                <span></span>
              </div>
            </div>
            <div class="col-lg-4 col-md-5 col-sm-12">
              <div class="box">
                <img src="/assets/img/introduce2-assay-management.png" alt="مدیریت آزمون" />
                <p>مدیریت آزمون</p>
                <span></span>
              </div>
              <div class="box">
                <img
                  src="/assets/img/introduce2-auto-correct-answer-sheet.png"
                  alt="تصحیح خودکار پاسخنامه"
                />
                <p>تصحیح خودکار پاسخنامه</p>
                <span></span>
              </div>
              <div class="box">
                <img src="/assets/img/introduce2-question-bank.png" alt="بانک سوال" />
                <p>بانک سوال</p>
                <span></span>
              </div>
              <div class="box">
                <img src="/assets/img/introduce2-assay-analysis.png" alt="تحلیل آزمون" />
                <p>تحلیل آزمون</p>
                <span></span>
              </div>
              <div class="box">
                <img src="/assets/img/introduce2-assay-operation.png" alt="عملیات آزمون" />
                <p>عملیات آزمون</p>
                <span></span>
              </div>
            </div>
          </section>
        </div>

        <div class="col-12 any-question-section">
          <section class="row">
            <p class="any-question-text col-12">سوالی دارید</p>
            <div class="col-8">
              <p>سامانه آموزشی چکونه کار میکند؟</p>
              <p>
                چه امکاناتی در این سامانه وجود دارد؟
                چقدر باید هزینه کرد؟
                چه سخت افزاری نیاز است؟...
              </p>
              <p>
                به قسمت
                <span class="link-text">پرسش های متداول و دربازه سامانه آموزشی</span> بروید
                و پاسخ پرسش های خود را بیابید
              </p>
              <p class="link-text">در صورتی که ابهامی برای شما وجود دارد, با ما تماس بگیرید.</p>
            </div>
            <div class="col-4">
              <img src="assets/img/any-question-section.png" alt="سوالی دارید؟" />
            </div>
          </section>
        </div>

        <div class="col-12 login-section">
          <h3>به راحتی در سامانه آموزشی ثبت نام کنید.</h3>
          <section class="row justify-center">
            <div class="col">
              <div class="box float-right">
                <p>هم اکنون ثبت نام کنید.</p>
                <span></span>
              </div>
            </div>
            <div class="col">
              <img src="/assets/img/login-section.png" alt="ثبت نام" />
            </div>
          </section>
        </div>

        <div class="col-12 footer-section"></div>
      </section>
    </q-page-container>
  </q-layout>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { vxm } from "src/store";
@Component({})
export default class HomeVue extends Vue {
  //### data ###
  //--------------------------------------------------
  //### methods ###
  //--------------------------------------------------
}
</script>

<style>
.home {
  background-color: white;
}
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
  height: auto;
  width: 100%;
  max-height: 800px;
  max-width: 100%;
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

.home .introduce-section .right-side .box {
  float: left;
}

.home .introduce-section .box {
  margin-top: 30px;
  width: 540px;
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

@media screen and (min-width: 768px) {
  .home .introduce-section .right-side .box {
    float: right;
  }
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

/* introduce2 */
.home .introduce2-section {
  margin-bottom: 100px;
  background-color: white;
}
.home .introduce2-section h3 {
  color: #464646;
  text-align: center;
  font-weight: bold;
  font-size: 40px;
}

.home .introduce2-section .box {
  margin: 70px auto 0 auto;
  border: 3px solid black;
  border-radius: 6px;
  position: relative;
  width: 310px;
  height: 75px;
  padding-top: 5px;
}

.home .introduce2-section .box p {
  color: #0072bc;
  font-size: 30px;
  font-weight: bold;
  text-align: center;
}

.home .introduce2-section .box img {
  width: 60px;
  height: 67px;
  position: absolute;
  top: -55px;
}

.home .introduce2-section .box span {
  width: 30px;
  height: 30px;
  position: absolute;
  border: 3px solid black;
  border-radius: 0 0 8px 0;
  left: calc(50% - 15px);
  top: 54px;
  border-color: transparent black black transparent;
  background-color: white;
  -ms-transform: rotate(45deg);
  -webkit-transform: rotate(45deg);
  transform: rotate(45deg);
}
/* --- */

/* any-question-section */
.home .any-question-section {
  background-color: #f36f21;
  color: white;
  font-size: 30px;
  font-weight: bold;
  padding: 90px 65px 0 65px;
}

.home .any-question-section .any-question-text {
  font-size: 60px;
  color: #005baa;
  font-weight: bold;
  text-align: center;
}

.home .any-question-section .link-text {
  color: #005baa;
}

.home .any-question-section img {
  width: 270px;
  height: 270px;
}
/* --- */

/* login-section */
.home .login-section {
  margin-bottom: 75px;
}

.home .login-section h3 {
  color: #464646;
  text-align: center;
  font-weight: bold;
  font-size: 40px;
}

.home .login-section .box {
  border: 3px solid #86bf3c;
  border-radius: 6px;
  background-color: #86bf3c;
  position: relative;
  width: 300px;
  height: 72px;
  padding-top: 5px;
  display: inline-block;
  margin: 22px 10px 0 0;
}

.home .login-section p {
  color: white;
  font-size: 30px;
  font-weight: bold;
  text-align: center;
}

.home .login-section .box span {
  width: 30px;
  height: 30px;
  position: absolute;
  border: 3px solid #86bf3c;
  border-radius: 0 0 8px 0;
  left: calc(50% - 15px);
  top: 54px;
  border-color: transparent #86bf3c #86bf3c transparent;
  background-color: #86bf3c;
  -ms-transform: rotate(45deg);
  -webkit-transform: rotate(45deg);
  transform: rotate(45deg);
}

.home .login-section img {
  width: 200px;
  height: 130px;
  margin: 0 0 0 10px;
}
/* --- */

.home .footer-section {
  height: 110px;
  background-color: #0072bc;
}
</style>

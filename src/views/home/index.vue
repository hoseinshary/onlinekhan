<template>
  <q-layout view="lHh Lpr lFf" class="home">
    <div>
      <div
        class="topnav"
        id="myTopnav"
        style="z-index: 1; position: absolute; width: 100%; padding: 30px"
      >
        <a href="#"
          ><img src="assets/img/home/1/logo1.png" style="width: 120px"
        /></a>
        <a href="#" class="animation">آزمون</a>
        <a href="#" class="animation">آموزش</a>
        <a href="#" class="animation">مشاوره</a>
        <a href="#" class="animation">لیست قیمت</a>
        <a href="#" class="animation">کلاس های آموزشی</a>
        <a href="/user/register" class="animation" style="float: left"
          ><i class="fa fa-pencil-square q-mx-xs"></i>ثبت نام</a
        >
        <a href="/user/login" class="animation" style="float: left"
          ><i class="fa fa-user q-mx-xs"></i>ورود</a
        >
           <a href="/resume/registration" class="animation" style="float: left"
          ><i class="fa fa-handshake-o q-mx-xs"></i>همکاری با ما</a
        >
        <a href="javascript:void(0);" class="icon" @click="myFunction">
          <i id="menuIcon" class="fa fa-bars"></i>
        </a>
      </div>
      <div class="col-12 main-image-section">
        <img src="assets/img/home/1/home-main-img.png" alt="img" />
      </div>
    </div>

    <q-page-container class="col-12">
      <section class="row">
        <div class="row col-12">
          <div class="col-md-12 text-center">
            <h5 class="q-ma-none text-black" style="font-weight: bolder">
              سامانه آموزشی چگونه به شما کمک می کند؟
            </h5>
          </div>
          <div class="col-md-5 text-center">
            <img
              src="assets/img/home/2/2.png"
              alt="img"
              style="max-width: 70%"
            />
          </div>
          <div class="col-md-7 col-12 row" style="padding-top: 60px">
            <div class="col-6 q-pr-xs">
              <div
                class="box"
                style="
                  text-align: left;
                  margin-bottom: 25px;
                  overflow: hidden;
                  white-space: nowrap;
                  height: 80px;
                "
              >
                مشاوره هوشمند
                <img
                  src="/assets/img/home/2/icons-2.png"
                  alt="مشاوره هوشمند"
                  style="
                    width: 60px;
                    max-width: 25%;
                    top: 20px;
                    position: relative;
                  "
                />
              </div>
              <div
                class="box"
                style="
                  text-align: left;
                  margin-bottom: 25px;
                  overflow: hidden;
                  white-space: nowrap;
                  height: 80px;
                "
              >
                بانک سوالات به روز
                <img
                  src="/assets/img/home/2/icons-4.png"
                  alt="بانک سوالات به روز"
                  style="
                    width: 60px;
                    max-width: 25%;
                    top: 20px;
                    position: relative;
                  "
                />
              </div>
              <div
                class="box"
                style="
                  text-align: left;
                  margin-bottom: 25px;
                  overflow: hidden;
                  white-space: nowrap;
                  height: 80px;
                "
              >
                تحلیل و مقایسه آزمون ها
                <img
                  src="/assets/img/home/2/icons-6.png"
                  alt="تحلیل و مقایسه آزمون ها"
                  style="
                    width: 60px;
                    max-width: 25%;
                    top: 20px;
                    position: relative;
                  "
                />
              </div>
            </div>
            <div class="col-6 q-pl-xs">
              <div
                class="box"
                style="
                  text-align: right;
                  margin-bottom: 25px;
                  overflow: hidden;
                  white-space: nowrap;
                  height: 80px;
                "
              >
                <img
                  src="/assets/img/home/2/icons-1.png"
                  alt="مدیریت کلیه مراحل آموزشی"
                  style="
                    width: 60px;
                    max-width: 25%;
                    top: 20px;
                    position: relative;
                  "
                />
                مدیریت کلیه مراحل آموزشی
              </div>
              <div
                class="box"
                style="
                  text-align: right;
                  margin-bottom: 25px;
                  overflow: hidden;
                  white-space: nowrap;
                  height: 80px;
                "
              >
                <img
                  src="/assets/img/home/2/icons-3.png"
                  alt="صفحه اختصاصی برای هر کاربر"
                  style="
                    width: 60px;
                    max-width: 25%;
                    top: 20px;
                    position: relative;
                  "
                />
                صفحه اختصاصی برای هر کاربر
              </div>
              <div
                class="box"
                style="
                  text-align: right;
                  margin-bottom: 25px;
                  overflow: hidden;
                  white-space: nowrap;
                  height: 80px;
                "
              >
                <img
                  src="/assets/img/home/2/icons-5.png"
                  alt="برگذاری آزمون آنلاین"
                  style="
                    width: 60px;
                    max-width: 25%;
                    top: 20px;
                    position: relative;
                  "
                />
                برگذاری آزمون آنلاین
              </div>
            </div>
          </div>
        </div>

        <!-- <div class="col-12 introduce-section">
          <h3>سامانه آموزشی چگونه به شما کمک می کند؟</h3>
          <section class="row justify-center">
            <div class="col-md-6 col-lg-5 col-sm-12 right-side">
              <div class="box">
                <img
                  src="/assets/img/home-intelligent-consult.png"
                  alt="مشاوره هوشمند"
                />
                <p>مشاوره هوشمند</p>
              </div>
              <div class="box">
                <img
                  src="/assets/img/home-updated-question-bank.png"
                  alt="بانک سوالات به روز"
                />
                <p>بانک سوالات به روز</p>
              </div>
              <div class="box">
                <img
                  src="/assets/img/home-assay-judgment.png"
                  alt="تحلیل و مقایسه آزمون ها"
                />
                <p>تحلیل و مقایسه آزمون ها</p>
              </div>
            </div>
            <div class="col-md-6 col-lg-5 col-sm-12">
              <div class="box">
                <img
                  src="/assets/img/home-step-management.png"
                  alt="مدیریت کلیه مراحل آموزشی"
                />
                <p>مدیریت کلیه مراحل آموزشی</p>
              </div>
              <div class="box">
                <img
                  src="/assets/img/home-dedicate-page.png"
                  alt="صفحه اختصاصی برای هر کاربر"
                />
                <p>صفحه اختصاصی برای هر کاربر</p>
              </div>
              <div class="box">
                <img
                  src="/assets/img/home-online-assay.png"
                  alt="برگذاری آزمون آنلاین"
                />
                <p>برگذاری آزمون آنلاین</p>
              </div>
            </div>
          </section>
        </div> -->

        <div class="col-12 counter-section">
          <section class="row" style="direction: ltr">
            <div class="col-md-4 col-12 text-center">
              <img
                src="/assets/img/home/3/3-2.png"
                alt="برگذاری آزمون آنلاین"
                style="height: 350px"
              />
            </div>
            <div class="col-md-8 col-12 row text-center">
              <div class="col-4 text-center">
                <div class="box">
                  <img src="/assets/img/home/3/3-icons-1.png" alt="داوطلب" />
                  <span>33</span>
                  <p>داوطلب</p>
                </div>
              </div>
              <div class="col-4 text-center">
                <div class="box">
                  <img src="/assets/img/home/3/3-icons-2.png" alt="مشتری" />
                  <span>8877</span>
                  <p>سوالات</p>
                </div>
              </div>
              <div class="col-4 text-center">
                <div class="box">
                  <img src="/assets/img/home/3/3-icons-3.png" alt="آزمون" />
                  <span>8</span>
                  <p>آزمون</p>
                </div>
              </div>
            </div>
          </section>
        </div>

        <div class="col-12 introduce2new-section">
          <section class="row" style="direction: ltr">
            <div class="col-lg-5 col-12 text-center">
              <img
                src="/assets/img/home/4/4-2.png"
                style="height: 450px; margin-left: 120px; margin-top: 50px"
              />
            </div>
            <div
              class="col-lg-7 col-12 row text-center"
              style="font-size: 20px; color: black"
            >
              <div
                class="col-12"
                style="
                  align-self: flex-end;
                  text-shadow: 0px 1px, 1px 0px, 1px 1px;
                "
              >
                ویژگی های سامانه آموزشی
              </div>
              <div class="col-6 q-pl-xs">
                <div
                  class="box"
                  style="
                    text-align: right;
                    margin-bottom: -10px;
                    overflow: hidden;
                    white-space: nowrap;
                    height: 80px;
                  "
                >
                  صفحه اختصاصی مشتری
                  <img
                    src="/assets/img/home/4/4-icon-1.png"
                    alt="صفحه اختصاصی مشتری"
                    style="
                      width: 60px;
                      max-width: 25%;
                      top: 20px;
                      position: relative;
                    "
                  />
                </div>
                <div
                  class="box"
                  style="
                    text-align: right;
                    margin-bottom: -10px;
                    overflow: hidden;
                    white-space: nowrap;
                    height: 80px;
                  "
                >
                  تصحیح خودکار پاسخنامه
                  <img
                    src="/assets/img/home/4/4-icon-3.png"
                    alt="تصحیح خودکار پاسخنامه"
                    style="
                      width: 60px;
                      max-width: 25%;
                      top: 20px;
                      position: relative;
                    "
                  />
                </div>
                <div
                  class="box"
                  style="
                    text-align: right;
                    margin-bottom: -10px;
                    overflow: hidden;
                    white-space: nowrap;
                    height: 80px;
                  "
                >
                  نمایندگی
                  <img
                    src="/assets/img/home/4/4-icon-5.png"
                    alt="نمایندگی"
                    style="
                      width: 60px;
                      max-width: 25%;
                      top: 20px;
                      position: relative;
                    "
                  />
                </div>
                <div
                  class="box"
                  style="
                    text-align: right;
                    margin-bottom: -10px;
                    overflow: hidden;
                    white-space: nowrap;
                    height: 80px;
                  "
                >
                  تحلیل آزمون
                  <img
                    src="/assets/img/home/4/4-icon-7.png"
                    alt="تحلیل آزمون"
                    style="
                      width: 60px;
                      max-width: 25%;
                      top: 20px;
                      position: relative;
                    "
                  />
                </div>
                <div
                  class="box"
                  style="
                    text-align: right;
                    margin-bottom: -10px;
                    overflow: hidden;
                    white-space: nowrap;
                    height: 80px;
                  "
                >
                  عملیات آزمون
                  <img
                    src="/assets/img/home/4/4-icon-9.png"
                    alt="عملیات آزمون"
                    style="
                      width: 60px;
                      max-width: 25%;
                      top: 20px;
                      position: relative;
                    "
                  />
                </div>
              </div>
              <div class="col-6 q-pr-xs">
                <div
                  class="box"
                  style="
                    text-align: left;
                    margin-bottom: -10px;
                    overflow: hidden;
                    white-space: nowrap;
                    height: 80px;
                  "
                >
                  <img
                    src="/assets/img/home/4/4-icon-2.png"
                    alt="ثبت اطلاعات"
                    style="
                      width: 60px;
                      max-width: 25%;
                      top: 20px;
                      position: relative;
                    "
                  />
                  ثبت اطلاعات
                </div>
                <div
                  class="box"
                  style="
                    text-align: left;
                    margin-bottom: -10px;
                    overflow: hidden;
                    white-space: nowrap;
                    height: 80px;
                  "
                >
                  <img
                    src="/assets/img/home/4/4-icon-4.png"
                    alt="مدیریت آزمون"
                    style="
                      width: 60px;
                      max-width: 25%;
                      top: 20px;
                      position: relative;
                    "
                  />
                  مدیریت آزمون
                </div>
                <div
                  class="box"
                  style="
                    text-align: left;
                    margin-bottom: -10px;
                    overflow: hidden;
                    white-space: nowrap;
                    height: 80px;
                  "
                >
                  <img
                    src="/assets/img/home/4/4-icon-6.png"
                    alt="بانک سوال"
                    style="
                      width: 60px;
                      max-width: 25%;
                      top: 20px;
                      position: relative;
                    "
                  />
                  بانک سوال
                </div>
                <div
                  class="box"
                  style="
                    text-align: left;
                    margin-bottom: -10px;
                    overflow: hidden;
                    white-space: nowrap;
                    height: 80px;
                  "
                >
                  <img
                    src="/assets/img/home/4/4-icon-8.png"
                    alt="تحلیل سوال"
                    style="
                      width: 60px;
                      max-width: 25%;
                      top: 20px;
                      position: relative;
                    "
                  />
                  تحلیل سوال
                </div>
                <div
                  class="box"
                  style="
                    text-align: left;
                    margin-bottom: -10px;
                    overflow: hidden;
                    white-space: nowrap;
                    height: 80px;
                  "
                >
                  <img
                    src="/assets/img/home/4/4-icon-10.png"
                    alt="پیشنیاز"
                    style="
                      width: 60px;
                      max-width: 25%;
                      top: 20px;
                      position: relative;
                    "
                  />
                  پیشنیاز
                </div>
              </div>
            </div>
          </section>
          <!-- <h3>سامانه آموزشی چگونه به شما کمک می کند؟</h3>
          <section class="row justify-center">
            <div class="col-lg-4 col-md-5 col-sm-12">
              <div class="box">
                <img
                  src="/assets/img/introduce2-register-information.png"
                  alt="ثبت اطلاعات"
                />
                <p>ثبت اطلاعات</p>
                <span></span>
              </div>
              <div class="box">
                <img
                  src="/assets/img/introduce2-dedicate-page.png"
                  alt="صفحه اختصاصی مشتری"
                />
                <p>صفحه اختصاصی مشتری</p>
                <span></span>
              </div>
              <div class="box">
                <img src="/assets/img/introduce2-delegate.png" alt="نمایندگی" />
                <p>نمایندگی</p>
                <span></span>
              </div>
              <div class="box">
                <img
                  src="/assets/img/introduce2-question-judgment.png"
                  alt="تحلیل سوال"
                />
                <p>تحلیل سوال</p>
                <span></span>
              </div>
              <div class="box">
                <img
                  src="/assets/img/introduce2-prerequisite.png"
                  alt="پیش نیاز"
                />
                <p>پیش نیاز</p>
                <span></span>
              </div>
            </div>
            <div class="col-lg-4 col-md-5 col-sm-12">
              <div class="box">
                <img
                  src="/assets/img/introduce2-assay-management.png"
                  alt="مدیریت آزمون"
                />
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
                <img
                  src="/assets/img/introduce2-question-bank.png"
                  alt="بانک سوال"
                />
                <p>بانک سوال</p>
                <span></span>
              </div>
              <div class="box">
                <img
                  src="/assets/img/introduce2-assay-analysis.png"
                  alt="تحلیل آزمون"
                />
                <p>تحلیل آزمون</p>
                <span></span>
              </div>
              <div class="box">
                <img
                  src="/assets/img/introduce2-assay-operation.png"
                  alt="عملیات آزمون"
                />
                <p>عملیات آزمون</p>
                <span></span>
              </div>
            </div>
          </section> -->
        </div>

        <div class="col-12 any-question-section">
          <section class="row" style="direction: ltr">
            <div class="col-lg-4 col-12">
              <img src="/assets/img/home/5/5-1.png" alt="سوالی دارید؟" />
            </div>
            <div class="col-lg-8 col-12" style="direction: rtl">
              <p
                style="
                  text-shadow: 0px 1px, 1px 0px, 1px 1px;
                  direction: ltr;
                  font-size: 30px;
                  margin-left: 80px;
                "
              >
                سامانه آموزشی چکونه کار میکند؟
              </p>
              <p style="font-size: 20px">
                چه امکاناتی در این سامانه وجود دارد؟ چقدر باید هزینه کرد؟ چه سخت
                افزاری نیاز است؟
              </p>
              <p style="font-size: 20px">
                به قسمت
                <span class="link-text"
                  >پرسش های متداول و دربازه سامانه آموزشی</span
                >
                بروید و پاسخ پرسش های خود را بیابید
              </p>
              <p class="link-text" style="font-size: 20px; margin: 0">
                در صورتی که ابهامی برای شما وجود دارد, با ما تماس بگیرید.
              </p>
            </div>
          </section>
        </div>

        <div class="col-12 row login-section">
          <div class="col-md-6 col-12" style="height: 10%;">
            <img
              src="/assets/img/home/6/6-1-2.png"
              alt="ثبت نام"
              style="width: 65%; padding: 50px 50px 20px 0"
            />

            <div
              class="box float-right"
              style="position: relative; bottom: 250px; left: 50px"
            >
              <p>هم اکنون ثبت نام کنید.</p>
              <span></span>
            </div>
            <div
              class="float-right"
              style="
                text-shadow: 0px 1px, 1px 0px, 1px 1px;
                font-size: 25px;
                color: black;
                position: relative;
                bottom: 100px;
                left: 15px;
              "
            >
              به راحتی در سامانه آموزشی ثبت نام کنید
            </div>
          </div>
          <div class="col-md-6 col-12">
            <img
              src="/assets/img/home/6/6-2.png"
              alt="ثبت نام"
              style="width: 75%; padding: 50px 135px 20px 0"
            />
          </div>

          <!-- <h3>به راحتی در سامانه آموزشی ثبت نام کنید.</h3>
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
          </section> -->
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

  myFunction() {
    let menu = document.getElementById("myTopnav") || new HTMLElement();
    let icon = document.getElementById("menuIcon") || new HTMLElement();
    if (menu.className === "topnav") {
      menu.className = "topnav responsive";
      icon.className = "fa fa-close";
    } else {
      menu.className = "topnav";
      icon.className = "fa fa-bars";
    }
  }
}
</script>

<style>
.topnav {
  overflow: hidden;
  z-index: 1;
  position: absolute;
  width: 100%;
  padding: 30px;
  /* background-color: #333; */
}

.topnav a {
  float: left;
  display: block;
  color: white;
  text-align: center;
  padding: 14px 16px;
  text-decoration: none;
  font-size: 15px;
}
.topnav.responsive a.animation {
  padding: 5px 16px;
}
.topnav.responsive a {
  background: #0000009e;
  float: none !important;
}

.topnav a:hover {
  /* background-color: #ddd; */
  color: black;
}

.topnav a.active {
  background-color: #4caf50;
  color: white;
}

.topnav .icon {
  display: none;
}

@media screen and (max-width: 850px) {
  .topnav a.animation {
    margin-top: -25%;
  }
  .topnav a.icon {
    float: right;
    display: block;
  }
  .topnav {
    padding: 0px !important;
  }
}

@media screen and (max-width: 850px) {
  .topnav.responsive a {
    text-align: left;
  }
  .topnav.responsive .icon {
    color: white;
    position: absolute;
    right: 0;
    top: 0;
  }
  .topnav.responsive a.animation {
    margin-top: 0;
    transition: 0.2s;
  }
}
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
  /* background-color: white; */
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
  /* background-image: url("../../assets/img/logo1.png"); */
  background-repeat: no-repeat;
  background-size: contain;
  height: 70px;
  max-width: 100%;
  width: 180px;
  margin-left: 50px !important;
}
/* --- */

/* main-image */
/* .home .main-image-section {
  margin-bottom: 70px;
} */

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
  background-color: #fbf9fb;
}

.home .counter-section .box img {
  width: 60px;
  height: 60px;
  display: block;
  margin: 50px auto 30px auto;
}

.home .counter-section .box span {
  color: #f36a46;
  font-size: 60px;
  font-weight: bold;
  display: block;
  line-height: 1.3;
  /* margin-bottom: 25px; */
}

.home .counter-section .box p {
  color: black;
  font-size: 20px;
  font-weight: bold;
  display: block;
  /* margin-bottom: 70px; */
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
  background-color: #21a6f3;
  color: white;
  font-size: 30px;
  font-weight: bold;
  padding: 60px 0px 0 80px;
}

.home .any-question-section .any-question-text {
  font-size: 60px;
  color: yellow;
  font-weight: bold;
  text-align: center;
}

.home .any-question-section .link-text {
  color: yellow;
}

.home .any-question-section img {
  /* width: 270px; */
  height: 230px;
  margin-top: 5px;
}
/* --- */

/* login-section */
/* .home .login-section {
  margin-bottom: 75px;
} */

.home .login-section h3 {
  color: #464646;
  text-align: center;
  font-weight: bold;
  font-size: 40px;
}

.home .login-section .box {
  border: 3px solid #ff9800;
  border-radius: 6px;
  background-color: #ff9800;
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
  border: 3px solid #ff9800;
  border-radius: 0 0 8px 0;
  left: calc(50% - 15px);
  top: 54px;
  border-color: transparent #ff9800 #ff9800 transparent;
  background-color: #ff9800;
  -ms-transform: rotate(45deg);
  -webkit-transform: rotate(45deg);
  transform: rotate(45deg);
}

/* .home .login-section img {
  width: 200px;
  height: 130px;
  margin: 0 0 0 10px;
} */
/* --- */
@media screen and (max-width: 1190px) {
  div.any-question-section img {
    margin-top: 120px !important;
    height: 175px !important;
  }
}
.home .footer-section {
  height: 110px;
  background-color: #0072bc;
}
</style>

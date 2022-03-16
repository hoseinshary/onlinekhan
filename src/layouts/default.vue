<template >
  <div class="background">
    <q-layout view="lHh Lpr lFf">

      <q-layout-header reveal>
        <div class="top-border"></div>
        <q-toolbar class="toolbar-header1">

          <q-btn
            flat
            dense
            round
            @click="leftDrawerOpen = !leftDrawerOpen"
            aria-label="Menu"
            color="black"
          >
              <q-icon name="menu" />
          </q-btn>

          <router-link to="/panel/adminpanel" class="logo"></router-link>
        
          <!-- <q-search class="search-box" icon="search" v-model="searchBox" float-label="عبارت موردنظر خود را بنویسید"/> -->
          
          <div class="profile q-ml-auto q-mr-lg ">
                <div class="row justify-center align-center">
                    <img
                    :src="$q.localStorage.get.item('ProfilePic')"
                    class="profile-image"
                    alt="profile picture"
                  />
                <q-btn flat dense class="q-mx-sm name-profile">
                  <!-- <q-icon name="account_circle" /> -->
                  {{ $q.localStorage.get.item("FullName") }}
                </q-btn>
                <q-btn @click="showProfileMenu =!showProfileMenu" flat dense>
                  <q-icon name="expand_more" />
                </q-btn>
                <div v-if="showProfileMenu" class="profile-menu">
                    <q-list>
                      <q-item  label="ویرایش اطلاعات کابری">
                        <q-btn flat>
                        ویرایش اطلاعات کاربری
                        </q-btn>
                      </q-item>
                      <q-item  label=" کیف پول">
                        <q-btn flat>
                          <div class="" >
                            <span> کیف پول</span>
                            <span class="charge">
                              {{ getCredit }}
                            </span>
                          </div>
                        </q-btn>
                      </q-item>
                      <q-item  label="خروج از حساب کاربری">
                        <q-btn @click="logout" flat>
                          خروج از حساب کاربری
                        </q-btn>
                      </q-item>
                    </q-list>
                </div>
              </div>
          </div>

        </q-toolbar>

        <q-toolbar class="toolbar-header2">

          <!-- navbar - clickable -->
            <div class="navbar">
              <dropdown v-for="(menu,i) in menuList2"
              :key="menu.id"
              :id="i"
              :title="menu.name"
              :items="menu.items"
              :isOpen="menu.subnavActive"
              @toggleSubnav="toggleSubnav1(i)" 
              @closeSubmenu="closeSub"
              >
              </dropdown>
            </div>


          <q-toolbar-title class="title">{{
            $q.localStorage.get.item("title")
          }}
          </q-toolbar-title>
          <router-link to="/panel/adminpanel"
            ><img
              src="/assets/img/header/default.png"
              class="header-image"
              alt="header image"
          /></router-link>

        </q-toolbar>

      </q-layout-header>

      <q-layout-drawer
        v-model="leftDrawerOpen"
        :overlay="true"
        side="left"
        :mini="false"
        behavior="mobile"
        class="layout-drawer"
        @click.capture="drawerClick"
      >
        <q-list no-border link inset-delimiter>
          <q-list-header>{{ siteName }}</q-list-header>
          <q-collapsible
            group="sideMenu"
            v-for="menu in menuList"
            :key="menu.ModuleId"
            :label="menu.ModuleName"
          >
            <router-link
              v-for="item in subMenuList.filter(
                (x) => x.ModuleId == menu.ModuleId
              )"
              :key="item.EnName"
              :to="item.EnName"
            >
              <q-item>
                <!-- <q-item-side icon='map' /> -->
                <!--"item.Icon" />-->
                <q-item-main :label="item.FaName" sublabel color="white" />
              </q-item>
            </router-link>
          </q-collapsible>
        </q-list>

      </q-layout-drawer>

      <q-page-container>
        <br />
        <div class="row justify-center q-mt-lg"  @click="closeSub">
          <transition
            name="transitions"
            enter-active-class="animated bounceInDown"
            leave-active-class="animated bounceOutUp"
            mode="out-in"
          >
            <router-view />
          </transition>
          <q-btn
            v-back-to-top.animate="{ offset: 500, duration: 200 }"
            round
            color="primary"
            class="fixed-bottom-left animate-pop"
            style="margin: 15px 15px 15px 15px"
          >
            <q-icon name="keyboard_arrow_up" />
          </q-btn>
        </div>
      </q-page-container>
      
    </q-layout>
  </div>
</template>

<script>
import util from "src/utilities";
import dropdown from "./dropdown.vue";
import { vxm } from "src/store";

export default {
  components:{
    dropdown
  },
  data() {
    return {
      siteName: "آنلاین خوان",
      leftDrawerOpen: true,
      miniState: false,
      menuList: null,
      subMenuList: null,
      showProfileMenu : false,
      //ati
      menuList2:[],
      charge:10000,
      defaultStore:vxm.defaultStore
    };
  },
  computed:{
    getCredit(){
      return ` ${this.defaultStore.getCREDIT } تومان`;
    }
  },
  methods: {
    drawerClick(e) {
      if (this.miniState) {
        this.miniState = false;
        e.stopPropagation();
      }
    },
    logout() {
      util.logout();
    },
     setmenuList2(){
       for(const menu in this.menuList){
         const temp = {};
         temp.id = this.menuList[menu].ModuleId;
         temp.name = this.menuList[menu].ModuleName;
         temp.items = this.subMenuList.filter((x) => x.ModuleId == this.menuList[menu].ModuleId);
         temp.subnavActive = false;
         this.menuList2.push(temp);
       }
     },
     toggleSubnav1(newId){
       let activeId = null;
       for(const menu in this.menuList2){  
         if(this.menuList2[menu].subnavActive === true ){
            activeId = menu;
           if(activeId === newId){
              this.menuList2[newId].subnavActive = false;
           }else{
             this.menuList2[newId].subnavActive = true;
             this.menuList2[activeId].subnavActive = false;
           }
           return
        }
       };
       this.menuList2[newId].subnavActive = true;
     },
     closeSub(){
       for(const item in this.menuList2){
         this.menuList2[item].subnavActive = false;
       }
     },
     handleScroll(){
       if(window.scrollY = (window.innerHeight /10)){
         this.closeSub();
       }
     }

  },
  async created() {
    this.menuList = await this.$q.localStorage.get.item("menuList");
    // console.log('menuLIst',this.menuList);
    this.subMenuList = await this.$q.localStorage.get.item("subMenuList");
    // console.log('submenuLIst',this.subMenuList);
    await this.setmenuList2();

    this.defaultStore.creditGetById();

    window.addEventListener("scroll", this.handleScroll);
  }
};
</script>

<style>
a{
  text-decoration: none !important;
}
.background{
  background-color: #fcfaf9;
}
.top-border{
  /* border-top: 14px solid #f36f21; */
  width: 100%;
  height:1rem;
  background: linear-gradient(180deg, hsla(22, 90%, 54%, 0.9) 20%, hsla(20, 33%, 98%, 0.6) 100%);
}

/* toolbar-header1 */
.toolbar-header1 {
  background-color: #fcfaf9 !important;
}

.toolbar-header1 .logo {
  background-image: url("../assets/img/logo1-old.png");
  background-repeat: no-repeat;
  background-size: contain;
  height: 60px;
  width: 170px;
  max-width: 100%;
  /* margin: 0 auto; */
}

.toolbar-header1 img.profile-image {
  width: 35px;
  height: 35px;
  border-radius: 50%;
}
/* --- */

/* toolbar-header2 */
.toolbar-header2 {
  background-color: #0A3F7E !important;
  color: #fcfaf9;
  overflow:inherit !important;
  padding-bottom: 0;
  padding-top: 0;
  padding-left: 1rem;
  padding-right: 1rem;
}

.toolbar-header2 .q-toolbar-title {
  text-align: right;
  margin-right: 5rem;
  font-size: 1.1rem !important;
  color: #48e5c2;
}

.toolbar-header2 img.header-image {
  position: fixed;
  top:5.5rem;
  width: 50px;
  right: 40px;
}

.toolbar-header2 .q-btn,
.toolbar-header2 .q-toolbar-title {
  font-size: 23px;
  font-weight: bold;
}

.toolbar-header2 .q-btn .q-icon {
  color: #f36f21;
}

.toolbar-header2 .q-btn:last-child {
  margin-right: 100px;
}

/* --- */

/* sidebar */
.layout-drawer aside {
  width: 220px;
  background-color: #3d566e;
  color: white;
}

.layout-drawer aside .q-list-header {
  font-weight: bold;
  color: white;
  text-align: center;
  border-bottom: 1px solid gray;
}

.layout-drawer aside .q-item {
  border-bottom: 1px solid gray;
}

.layout-drawer aside .q-item:hover {
  border-bottom: 2px solid white;
  background-color: #263544;
  padding-left: 25px;
  font-weight: bold;
  -webkit-transition: all 500ms; /* Safari */
  transition: all 500ms;
}

.layout-drawer aside .q-collapsible-sub-item {
  padding: 0;
}

.layout-drawer aside .q-collapsible-sub-item a {
  color: white;
  text-decoration: none;
}

.layout-drawer aside .q-collapsible-sub-item .q-item-label {
  color: white;
  padding-left: 15px;
}

.layout-drawer aside .q-collapsible i.q-icon {
  color: white;
}
/* --- */


.profile{
  color: #0A3F7E;
  position: relative;
}
.profile-menu{
  position: fixed;
  z-index: 100;
  margin-top:4.7rem;
  top: 0;
  color:#0A3F7E ;
  background-color: #fcfaf9;
  border: 2px solid #48e5c2;
  border-radius: 4px;
  width: max-content;
}
.profile-menu .q-item{
  padding: 0;
}
.profile-menu .charge{
  color: #f36f21;
  font-size: .75rem;
  margin-left: .5rem;
}
.profile .q-btn:hover,
.name-profile:hover{
  cursor: pointer;
  color: #48e5c2;
  background: transparent;
}
.q-focusable:focus {
  background-color: #fcfaf9;;
  color: #0A3F7E;
}


 /* The navigation menu */
.navbar {
  display: none;
}

.navbar a {
  float: left;
  font-size: 14px;
  color: white;
  text-align: center;
  padding: .5rem 1.2rem;
  text-decoration: none;
  cursor: pointer;
}

.subnav {
  position: relative;
  cursor: pointer;
}

.subnav .subnavbtn {
  font-size: 15px;
  border: none;
  outline: none;
  color: white;
  padding: 14px 16px;
  background-color: inherit;
  font-family: inherit;
  margin: 0;
}

.navbar a:hover, .subnav:hover .subnavbtn {
  color: #48e5c2;
}

.subnav-content {
  /* display: none; */
  position: absolute;
  left: 0;
  background-image:linear-gradient(180deg, hsla(213, 85%, 27%, 1) 0%, hsla(193, 100%, 43%, 1) 100%);
  /* background-image:linear-gradient(180deg, hsla(213, 85%, 27%, 1) 0%, hsla(22, 90%, 54%, 0.5) 100%); */
  z-index: 1;
  padding-bottom: 0;
  padding-top: 0;
  border-radius:0 0 4px 4px;
  width: 13vw;
}

.subnav-content a {
  width: max-content;
  color: #fcfaf9;
  text-decoration: none;
  border-left: 4px solid transparent;
  
}
.subnav-content a:hover {
  color: #48e5c2;
  border-color: #48e5c2 ;
}
/* .subnav:hover .subnav-content {
  display: block;
} */
@media screen and (min-width:768px){
  .navbar{
    display: flex;
  }
}
</style>
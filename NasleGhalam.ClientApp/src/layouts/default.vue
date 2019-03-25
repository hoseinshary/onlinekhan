<template>
  <q-layout view="lHh Lpr lFf">
    <q-layout-header>
      <q-toolbar class="toolbar-header" color>
        <q-btn flat dense round @click="leftDrawerOpen = !leftDrawerOpen" aria-label="Menu">
          <q-icon name="menu"/>
        </q-btn>

        <q-toolbar-title>
          {{siteName}}
          <div slot="subtitle">سامانه جامع کنکور</div>
        </q-toolbar-title>

        <q-btn flat dense class="q-mr-sm">
          <q-icon name="account_circle"/>
          {{fullName}}
        </q-btn>
        <q-btn @click="logout" flat dense>
          <q-icon name="exit_to_app"/>خروج
        </q-btn>
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
        <q-list-header>{{siteName}}</q-list-header>
        <q-collapsible
          group="sideMenu"
          v-for="menu in menuList"
          :key="menu.ModuleId"
          :label="menu.ModuleName"
        >
          <router-link
            v-for="item in subMenuList.filter(x=>x.ModuleId == menu.ModuleId)"
            :key="item.EnName"
            :to="item.EnName"
          >
            <q-item>
              <!-- <q-item-side icon='map' /> -->
              <!--"item.Icon" />-->
              <q-item-main :label="item.FaName" sublabel color="white"/>
            </q-item>
          </router-link>
        </q-collapsible>
      </q-list>
    </q-layout-drawer>

    <q-page-container class>
      <br>
      <div class="row justify-center q-mt-lg">
        <transition
          name="transitions"
          enter-active-class="animated bounceInDown"
          leave-active-class="animated bounceOutUp"
          mode="out-in"
        >
          <router-view/>
        </transition>
      </div>
    </q-page-container>
  </q-layout>
</template>

<script>
import util from "src/utilities";

export default {
  data() {
    return {
      siteName: "آنلاین خوان",
      leftDrawerOpen: true,
      miniState: false,
      menuList: null,
      subMenuList: null,
      fullName: ""
    };
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
    }
  },
  created: function() {
    this.fullName = this.$q.localStorage.get.item("FullName");
    this.menuList = this.$q.localStorage.get.item("menuList");
    this.subMenuList = this.$q.localStorage.get.item("subMenuList");
  }
};
</script>

<style>
.toolbar-header {
  background-color: #34495e;
}

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
</style>
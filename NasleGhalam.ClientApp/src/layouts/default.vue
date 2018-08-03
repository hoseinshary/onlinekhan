<template>
  <q-layout view="lHh Lpr lFf">
    <q-layout-header>
      <q-toolbar :glossy="$q.theme === 'mat'"
                 text-color="white"
                 color="cyan-10">
        <q-btn flat
               dense
               round
               @click="leftDrawerOpen = !leftDrawerOpen"
               aria-label="Menu">
          <q-icon name="menu" />
        </q-btn>

        <q-toolbar-title>
          نسل قلم
          <div slot="subtitle">سامانه جامع کنکور
          </div>
        </q-toolbar-title>
        <div class="gt-xs inline">
          <img src="/assets/img/User.jpg"
               class="img-circle"
               style="width: 20px;height: 20px;border-radius: 50%;"> {{FullName}}،
          خوش آمدید
          <a @click="logout"
             style="text-decoration: none;color: white;padding-right: 20px;cursor: pointer;">
            <q-icon name="assignment return" /> خروج </a>
        </div>
        <div class="lt-sm inline">
          <q-btn flat
                 dense
                 round
                 color="primary"
                 @click="showLogoutMenu = true"
                 style="color:white !important">
            <q-icon name="list" />
          </q-btn>
          <div>

            <q-popover v-model="showLogoutMenu"
                       style="border-radius: 5px;">
              <div style="padding:5px; background: rgb(32, 116, 119);border-radius: 5px;overflow: overlay;">
                <img src="/assets/img/User.jpg"
                     class="img-circle"
                     style="width: 20px;height: 20px;border-radius: 50%;">{{FullName}}،
                خوش آمدید
                <a @click="logout"
                   style="text-decoration: none;color: white;margin-right: 5px; cursor: pointer;">
                  <q-icon id="exitLtlMenu"
                          name="assignment return" /> خروج </a>
              </div>
            </q-popover>
          </div>
        </div>

      </q-toolbar>
    </q-layout-header>

    <q-layout-drawer v-model="leftDrawerOpen"
                     :overlay="true"
                     side="left"
                     :mini="miniState"
                     @click.capture="drawerClick"
                     :content-class="$q.theme === 'mat' ? 'bg-cyan-14' : null">
      <router-link to="/grade">
        <q-item>
          <q-item-side icon="receipt" />
          <q-item-main label="دوره تحصیلی" />
        </q-item>
      </router-link>
      <router-link to="/gradeLevel">
        <q-item>
          <q-item-side icon="reorder" />
          <q-item-main label="پایه تحصیلی" />
        </q-item>
      </router-link>
      <router-link to="/province">
        <q-item>
          <q-item-side icon="reorder" />
          <q-item-main label="استان" />
        </q-item>
      </router-link>
      <router-link to="/city">
        <q-item>
          <q-item-side icon="reorder" />
          <q-item-main label="شهر" />
        </q-item>
      </router-link>
      <router-link to="/lesson">
        <q-item>
          <q-item-side icon="reorder" />
          <q-item-main label="درس" />
        </q-item>
      </router-link>
      <router-link to="/role">
        <q-item>
          <q-item-side icon="reorder" />
          <q-item-main label="نقش" />
        </q-item>
      </router-link>
      <router-link to="/user">
        <q-item>
          <q-item-side icon="reorder" />
          <q-item-main label="کاربر" />
        </q-item>
      </router-link>
    </q-layout-drawer>

    <q-page-container>

      <br />
      <div class="row justify-center">
        <router-view />
      </div>

    </q-page-container>
  </q-layout>
</template>

<script>
import { openURL } from 'quasar';

export default {
  data() {
    return {
      leftDrawerOpen: true, //this.$q.platform.is.desktop,
      miniState: false,
      menuList: null,
      FullName: 'علیرضا اعتمادی',
      showLogoutMenu: false
    };
  },
  methods: {
    openURL,
    drawerClick(e) {
      // if in "mini" state and user
      // click on drawer, we switch it to "normal" mode
      if (this.miniState) {
        this.miniState = false;

        // notice we have registered an event with capture flag;
        // we need to stop further propagation as this click is
        // intended for switching drawer to "normal" mode only
        e.stopPropagation();
      }
    },
    logout() {
      this.$q.localStorage.remove('authList');
      this.$q.localStorage.remove('menuList');
      this.$q.localStorage.remove('Token');
      this.$q.localStorage.remove('FullName');
      this.$axios.defaults.headers.common['Token'] = '';
      this.$router.push('/user/login');
    }
  },
  created: function() {
    // this.FullName = this.$q.localStorage.get.item('FullName');
    // this.menuList = this.$q.localStorage.get.item('menuList');
    // window.$eventHub.on('logoutAxios', () => {
    //   this.logout();
    // });
  }
};
</script>

<style>
</style>

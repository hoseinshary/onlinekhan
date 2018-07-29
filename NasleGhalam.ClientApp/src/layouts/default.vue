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
    </q-layout-drawer>

    <q-page-container>

      <br />
      <div class="row">
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
      miniState: false
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
    }
  }
};
</script>

<style>
</style>

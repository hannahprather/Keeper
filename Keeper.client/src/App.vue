<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
    <keep-modal />
  </main>

  <footer></footer>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import KeepModal from "./components/KeepModal.vue"
export default {
  components: { KeepModal },
  name: 'App',
  setup() {
    return {
      appState: computed(() => AppState)
    }
  }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";
</style>

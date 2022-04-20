<template>
  <div class="about pt-2 px-4 pl-4">
    <img class="rounded" :src="account.picture" alt="" />
    <p>{{ account.email }}</p>
  </div>
  <div class="px-5">
    <h2>Vaults:</h2>
    {{}}
    <h2>Keeps:</h2>
  </div>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
export default {
  name: 'Account',
  setup() {
    return {
      account: computed(() => AppState.account)

      //get my vaults => with the keeps ive saved
      //get keeps ive made

    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>

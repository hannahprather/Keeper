<template>
  <div class="container">
    <div class="row justify-content-center">
      <keeps v-for="k in keeps" :key="k.id" :keep="k" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from "@vue/runtime-core"
import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { keepsService } from "../services/KeepsService"
import Keeps from "../components/Keeps.vue"
export default {
  components: { Keeps },
  name: 'Home',
  setup() {
    onMounted(async () => {
      try {
        await keepsService.getAll()
      } catch (error) {
        logger.log(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      keeps: computed(() => AppState.keeps)
    }
  }
}

</script>

<style scoped lang="scss">
</style>
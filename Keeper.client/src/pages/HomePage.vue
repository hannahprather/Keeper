<template>
  <div class="container">
    <div class="masonry-with-columns py-2">
      <div v-for="k in keeps" :key="k.id">
        <Keeps :keep="k" />
      </div>
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

<style lang="scss" scoped>
.masonry-with-columns {
  columns: 6 200px;
  column-gap: 1rem;
  div {
    display: inline-block;
    width: 100%;
  }
}
</style>>
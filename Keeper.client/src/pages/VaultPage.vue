<template>
  <h1>{{ activeVault.name }}</h1>
  <h5>{{ activeVault.description }}</h5>
</template>

<script>
import { computed, onMounted } from "@vue/runtime-core"
import { vaultsService } from "../services/VaultsService"
import { AppState } from "../AppState"
import { useRoute } from "vue-router"
export default {
  setup() {
    const route = useRoute()
    onMounted(async () => {
      await vaultsService.setActiveVault(route.params.Id)
    })
    return {
      activeVault: computed(() => AppState.activeVault)
    }
  }
}
</script>
<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-6 px-4">
        <h1>{{ activeVault.name }}</h1>
        <h5>{{ activeVault.description }}</h5>
      </div>
      <div class="col-6 pt-2">
        <i
          v-if="account.id == activeVault.creatorId"
          class="mdi mdi-delete selectable px-3"
          title="delete"
          @click="deleteVault(activeVault.id)"
        ></i>
      </div>
    </div>
    <div class="masonry-with-columns">
      <div v-for="vk in vaultKeeps" :key="vk.id">
        <div class="pt-4">
          <div class="card bg-dark p-0 text-white">
            <img :src="vk?.img" class="card-img p-0" alt="..." />
            <div class="card-img-overlay">
              <div class="selectable">
                <h5 class="card-title align-items-end">
                  {{ vk?.name }}
                </h5>
                <i
                  v-if="account.id == activeVault.creatorId"
                  class="mdi mdi-delete selectable px-3"
                  title="delete"
                  @click="deleteVaultKeep(vk.vaultKeepId)"
                ></i>
              </div>
              <p class="card-text"></p>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from "@vue/runtime-core"
import { vaultsService } from "../services/VaultsService"
import { AppState } from "../AppState"
import { useRoute } from "vue-router"
import { logger } from "../utils/Logger"
import { vaultKeepsService } from "../services/VaultKeepsService"
export default {
  setup() {
    const route = useRoute()
    onMounted(async () => {
      try {
        await vaultsService.setActiveVault(route.params.id)
      } catch (error) {
        logger.log(error, 'error')
      }
      try {
        await vaultKeepsService.getVaultKeeps(route.params.id)
      } catch (error) {
        logger.log(error, 'error')
      }


    })
    return {
      activeVault: computed(() => AppState.activeVault),
      vaultKeeps: computed(() => AppState.vaultKeeps),
      account: computed(() => AppState.account),
      async deleteVault(id) {
        logger.log("deleting from deleter", id)
        await vaultsService.deleteVault(id)
      },

      async deleteVaultKeep(id) {
        logger.log('deleting vault keeps', id)
        await vaultKeepsService.deleteVaultKeep(id);
      }
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
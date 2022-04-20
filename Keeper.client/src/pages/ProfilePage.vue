<template>
  <div class="container">
    <div class="p-4">
      <img height="300" class="w-35 rounded" :src="profile.picture" alt="" />
      <h1>{{ profile.name }}</h1>
    </div>
    <h2>Vaults:</h2>

    <div v-for="v in profileVaults" :key="v.id" @click="setActiveVault(v.id)">
      <div class="col-3">
        <div
          class="card text-white selectable elevation-2 bg-dark mb-3"
          style="max-width: 18rem"
        >
          <div class="card-body">
            <div class="d-flex flex-column align-items-center">
              <h5 class="card-title">{{ v.name }}</h5>
            </div>
          </div>
        </div>
      </div>
    </div>
    <h2>Keeps:</h2>
    <div class="masonry-with-columns py-2">
      <div v-for="k in profileKeeps" :key="k.id">
        <div class="pt-4">
          <div class="card bg-dark p-0 text-white">
            <img :src="k?.img" class="card-img p-0" alt="..." />
            <div class="card-img-overlay">
              <div class="selectable">
                <h5 class="card-title align-items-end">
                  {{ k?.name }}
                </h5>
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
import { useRoute } from "vue-router"
import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { profilesService } from '../services/ProfilesService.js'
import { vaultsService } from '../services/VaultsService.js'
import { router } from "../router"

export default {
  setup() {
    const route = useRoute()
    onMounted(async () => {
      try {
        await profilesService.getProfile(route.params.id)
        await profilesService.getProfileKeeps(route.params.id)
        await profilesService.getProfileVaults(route.params.id)

      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      profile: computed(() => AppState.profile),
      profileKeeps: computed(() => AppState.profileKeeps),
      profileVaults: computed(() => AppState.profileVaults),
      setActiveVault(id) {
        vaultsService.setActiveVault(id)
        router.push({ name: 'Vault', params: { id } })
      },

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
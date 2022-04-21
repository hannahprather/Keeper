<template>
  <Modal id="active-keep">
    <template #modal-body>
      <div class="container-fluid">
        <div class="row">
          <div class="col-md-6 p-4 rounded">
            <img
              height="500"
              class="w-100 object-fit-cover rounded"
              :src="activeKeep.img"
              alt=""
            />
          </div>
          <div class="col-md-6 p-2">
            <div class="mt-3">
              <h3 class="text-center">
                {{ activeKeep.name }}
              </h3>
              <div class="d-flex py-4 justify-content-around">
                <i class="mdi mdi-eye text-danger" alt="views">
                  {{ activeKeep.views }} </i
                ><i
                  v-if="account.id == activeKeep.creatorId"
                  class="mdi mdi-delete selectable px-3"
                  title="delete"
                  @click="deleteKeep(activeKeep.id)"
                ></i>
                <i class="mdi mdi-key text-danger" alk="saves">
                  {{ activeKeep.kept }}</i
                >
              </div>
              <p class="py-4">{{ activeKeep.description }}</p>
            </div>
            <!-- buttons -->
            <div class="">
              <div class="d-flex absolute1 w-100 align-items-center">
                <select
                  v-model="state.vaultId"
                  @change="addKeepToVault(activeKeep.id)"
                  name=""
                  id=""
                  class="h-25"
                  data-option-label="Select a Vault"
                  required
                >
                  <option disabled value="">Select a vault</option>
                  <option v-for="v in userVaults" :key="v.id" :value="v.id">
                    {{ v.name }}
                  </option>
                </select>

                <div
                  class="selectable p-2 d-flex"
                  @click="goToProfile(activeKeep.creator.id)"
                >
                  <img
                    height="50"
                    class="w-70 object-fit-cover rounded"
                    :src="activeKeep.creator?.picture"
                    alt=""
                  />
                  <p class="p-2 text-small">
                    {{ activeKeep.creator?.name }}
                  </p>
                </div>
              </div>
            </div>
            <!-- end buttons -->
          </div>
        </div>
      </div>
    </template>
  </Modal>
</template>


<script>
import { computed, reactive, ref } from '@vue/reactivity'
import { AppState } from '../AppState.js'
import { Modal } from 'bootstrap'
import { useRouter } from 'vue-router'
import { vaultKeepsService } from "../services/VaultKeepsService.js"
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
import { keepsService } from "../services/KeepsService.js"
export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const router = useRouter()
    const keep = ref({})
    const state = reactive({
      vaultId: null,
      keepId: null
    })
    return {
      state,
      userVaults: computed(() => AppState.myVaults),
      activeKeep: computed(() => AppState.activeKeep),
      account: computed(() => AppState.account),
      goToProfile(id) {
        Modal.getOrCreateInstance(document.getElementById('active-keep')).hide()
        router.push({ name: 'Profile', params: { id } })
      },
      async addKeepToVault(keepId) {
        try {
          state.keepId = keepId
          // logger.log(state)
          await vaultKeepsService.addKeepToVault(state)
          Pop.toast("added keep to vault")
        } catch (error) {
          logger.log(error)
        }

      },
      async deleteKeep(id) {
        logger.log("deleting from deleter", id)
        await keepsService.deleteKeep(id);
      },
    }
  }
}
</script>


<style lang="scss" scoped>
.right-col {
  max-height: 400px;
  overflow-y: auto;
}
.p-info {
  position: fixed;
  bottom: 20%;
}
.v-add {
  position: fixed;
  bottom: 30%;
}
.absolute1 {
  position: absolute;
  width: 50%;
  bottom: 10px;
}
</style>